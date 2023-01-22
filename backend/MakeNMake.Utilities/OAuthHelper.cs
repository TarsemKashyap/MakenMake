using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;

public class OAuthHelperTwitter
{
    public OAuthHelperTwitter() { }

    static string oauth_consumer_key = ConfigurationSettings.AppSettings["oauth_consumer_key"];//"w5LCgZnBYBmEFGVCv99g";
    static string oauth_consumer_secret = ConfigurationSettings.AppSettings["oauth_consumer_secret"];//"sO1R4AgXdI8h87f8yPAG7CZd5eNtUXOSWl7g2YaQPog";
    static string callbackUrl = ConfigurationSettings.AppSettings["callbackUrl"];//"http://127.0.0.1/twitshoppers/oauth/oauthreturn.aspx";

    #region (Changable) Do Not Change It
    static string REQUEST_TOKEN = "https://api.twitter.com/oauth/request_token";
    static string AUTHORIZE = "https://api.twitter.com/oauth/authorize";
    static string ACCESS_TOKEN = "https://api.twitter.com/oauth/access_token";


    public enum httpMethod
    {
        POST, GET
    }
    public string oauth_request_token { get; set; }
    public string oauth_access_token { get; set; }
    public string oauth_access_token_secret { get; set; }
    public string user_id { get; set; }
    public string screen_name { get; set; }
    public string oauth_error { get; set; }

    public string GetRequestToken()
    {
        HttpWebRequest request = FetchRequestToken(httpMethod.POST, oauth_consumer_key, oauth_consumer_secret);
        string result = getResponce(request);
        Dictionary<string, string> resultData = OAuthUtility.GetQueryParameters(result);
        if (resultData.Keys.Contains("oauth_token"))
            return resultData["oauth_token"];
        else
        {
            this.oauth_error = result;
            return "";
        }
    }
    public string GetAuthorizeUrl(string requestToken)
    {
        return string.Format("{0}?oauth_token={1}", AUTHORIZE, requestToken);
    }
    public void GetUserTwAccessToken(string oauth_token, string oauth_verifier)
    {
        HttpWebRequest request = FetchAccessToken(httpMethod.POST, oauth_consumer_key, oauth_consumer_secret, oauth_token, oauth_verifier);
        string result = getResponce(request);

        Dictionary<string, string> resultData = OAuthUtility.GetQueryParameters(result);
        if (resultData.Keys.Contains("oauth_token"))
        {
            this.oauth_access_token = resultData["oauth_token"];
            this.oauth_access_token_secret = resultData["oauth_token_secret"];
            this.user_id = resultData["user_id"];
            this.screen_name = resultData["screen_name"];
        }
        else
            this.oauth_error = result;
    }
    public void TweetOnBehalfOf(string oauth_access_token, string oauth_token_secret, string postData)
    {
        HttpWebRequest request = PostTwits(oauth_consumer_key, oauth_consumer_secret, oauth_access_token, oauth_token_secret, postData);
        string result = OAuthHelperTwitter.getResponce(request);
        Dictionary<string, string> dcResult = OAuthUtility.GetQueryParameters(result);
        if (dcResult["status"] != "200")
        {
            this.oauth_error = result;
        }

    }

    public string GetFriend()
    {
        oauth_access_token = HttpContext.Current.Session["twtoken"].ToString();
        oauth_access_token_secret = HttpContext.Current.Session["twsecret"].ToString();
        screen_name = HttpContext.Current.Session["twname"].ToString();
       return TwitsFriends(oauth_consumer_key, oauth_consumer_secret, oauth_access_token, oauth_access_token_secret, screen_name);
         
    }
    public string GetContact()
    {
        oauth_access_token = HttpContext.Current.Session["twtoken"].ToString();
        oauth_access_token_secret = HttpContext.Current.Session["twsecret"].ToString();
        screen_name = HttpContext.Current.Session["twname"].ToString();
        return TwitsContact(oauth_consumer_key, oauth_consumer_secret, oauth_access_token, oauth_access_token_secret, screen_name);

    }

    HttpWebRequest FetchRequestToken(httpMethod method, string oauth_consumer_key, string oauth_consumer_secret)
    {
        string OutUrl = "";
        string OAuthHeader = OAuthUtility.GetAuthorizationHeaderForPost_OR_QueryParameterForGET(new Uri(REQUEST_TOKEN), callbackUrl, method.ToString(), oauth_consumer_key, oauth_consumer_secret, "", "", out OutUrl);

        if (method == httpMethod.GET)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(OutUrl + "?" + OAuthHeader);
            request.Method = method.ToString();
            return request;
        }
        else if (method == httpMethod.POST)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(OutUrl);
            request.Method = method.ToString();
            request.Headers["Authorization"] = OAuthHeader;
            return request;
        }
        else
            return null;


    }
    HttpWebRequest FetchAccessToken(httpMethod method, string oauth_consumer_key, string oauth_consumer_secret, string oauth_token, string oauth_verifier)
    {
        string postData = "oauth_verifier=" + oauth_verifier;
        string AccessTokenURL = string.Format("{0}?{1}", ACCESS_TOKEN, postData);
        string OAuthHeader = OAuthUtility.GetAuthorizationHeaderForPost_OR_QueryParameterForGET(new Uri(AccessTokenURL), callbackUrl, method.ToString(), oauth_consumer_key, oauth_consumer_secret, oauth_token, "", out AccessTokenURL);

        if (method == httpMethod.GET)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(AccessTokenURL + "?" + OAuthHeader);
            request.Method = method.ToString();
            return request;
        }
        else if (method == httpMethod.POST)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(AccessTokenURL);
            request.Method = method.ToString();
            request.Headers["Authorization"] = OAuthHeader;

            byte[] array = Encoding.ASCII.GetBytes(postData);
            request.GetRequestStream().Write(array, 0, array.Length);
            return request;
        }
        else
            return null;

    }
    HttpWebRequest PostTwits(string oauth_consumer_key, string oauth_consumer_secret, string oauth_access_token, string oauth_token_secret, string postData)
    {
        postData = "trim_user=true&include_entities=true&status=" + postData;
        string updateStatusURL = "https://api.twitter.com/1/statuses/update.json?" + postData;

        string outUrl;
        string OAuthHeaderPOST = OAuthUtility.GetAuthorizationHeaderForPost_OR_QueryParameterForGET(new Uri(updateStatusURL), callbackUrl, httpMethod.POST.ToString(), oauth_consumer_key, oauth_consumer_secret, oauth_access_token, oauth_token_secret, out outUrl);

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(outUrl);
        request.Method = httpMethod.POST.ToString();
        request.Headers["Authorization"] = OAuthHeaderPOST;

        byte[] array = Encoding.ASCII.GetBytes(postData);
        request.GetRequestStream().Write(array, 0, array.Length);
        return request;

    }
    string TwitsFriends(string aoauth_consumer_key, string aoauth_consumer_secret, string oauth_access_token, string aoauth_token_secret, string ScreenName)
    {
        // oauth application keys
        var oauth_token = oauth_access_token;
        var oauth_token_secret = aoauth_token_secret;
        var oauth_consumer_key = aoauth_consumer_key;
        var oauth_consumer_secret = aoauth_consumer_secret;

        // oauth implementation details
        var oauth_version = "1.0";
        var oauth_signature_method = "HMAC-SHA1";

        // unique request details
        var oauth_nonce = Convert.ToBase64String(
            new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
        var timeSpan = DateTime.UtcNow
            - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        var oauth_timestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();

        // message api details
        var status = "Updating status via REST API if this works";
        var resource_url = "https://api.twitter.com/1.1/friends/list.json";
        var screen_name = ScreenName;
        // create oauth signature
        var baseFormat = "oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" +
                        "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}&screen_name={6}";

        var baseString = string.Format(baseFormat,
                                    oauth_consumer_key,
                                    oauth_nonce,
                                    oauth_signature_method,
                                    oauth_timestamp,
                                    oauth_token,
                                    oauth_version,
                                     Uri.EscapeDataString(screen_name)
                                    );

        baseString = string.Concat("GET&", Uri.EscapeDataString(resource_url), "&", Uri.EscapeDataString(baseString));

        var compositeKey = string.Concat(Uri.EscapeDataString(oauth_consumer_secret),
                                "&", Uri.EscapeDataString(oauth_token_secret));

        string oauth_signature;
        using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(compositeKey)))
        {
            oauth_signature = Convert.ToBase64String(
                hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(baseString)));
        }

        // create the request header
        var headerFormat = "OAuth oauth_nonce=\"{0}\", oauth_signature_method=\"{1}\", " +
                           "oauth_timestamp=\"{2}\", oauth_consumer_key=\"{3}\", " +
                           "oauth_token=\"{4}\", oauth_signature=\"{5}\", " +
                           "oauth_version=\"{6}\"";

        var authHeader = string.Format(headerFormat,
                                Uri.EscapeDataString(oauth_nonce),
                                Uri.EscapeDataString(oauth_signature_method),
                                Uri.EscapeDataString(oauth_timestamp),
                                Uri.EscapeDataString(oauth_consumer_key),
                                Uri.EscapeDataString(oauth_token),
                                Uri.EscapeDataString(oauth_signature),
                                Uri.EscapeDataString(oauth_version)
                        );


        // make the request

        ServicePointManager.Expect100Continue = false;

        var postBody = "screen_name=" + Uri.EscapeDataString(screen_name);//
        resource_url += "?" + postBody;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(resource_url);
        request.Headers.Add("Authorization", authHeader);
        request.Method = "GET";
        request.ContentType = "application/x-www-form-urlencoded";


        WebResponse response = request.GetResponse();
        return new StreamReader(response.GetResponseStream()).ReadToEnd();
    }
    string TwitsContact(string aoauth_consumer_key, string aoauth_consumer_secret, string oauth_access_token, string aoauth_token_secret, string ScreenName)
    {
        // oauth application keys
        var oauth_token = oauth_access_token;
        var oauth_token_secret = aoauth_token_secret;
        var oauth_consumer_key = aoauth_consumer_key;
        var oauth_consumer_secret = aoauth_consumer_secret;

        // oauth implementation details
        var oauth_version = "1.0";
        var oauth_signature_method = "HMAC-SHA1";

        // unique request details
        var oauth_nonce = Convert.ToBase64String(
            new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
        var timeSpan = DateTime.UtcNow
            - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        var oauth_timestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();

        // message api details
        var status = "Updating status via REST API if this works";
        var resource_url = "https://api.twitter.com/1.1/users/show.json";
        var screen_name = ScreenName;
        // create oauth signature
        var baseFormat = "oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" +
                        "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}&screen_name={6}";

        var baseString = string.Format(baseFormat,
                                    oauth_consumer_key,
                                    oauth_nonce,
                                    oauth_signature_method,
                                    oauth_timestamp,
                                    oauth_token,
                                    oauth_version,
                                     Uri.EscapeDataString(screen_name)
                                    );

        baseString = string.Concat("GET&", Uri.EscapeDataString(resource_url), "&", Uri.EscapeDataString(baseString));

        var compositeKey = string.Concat(Uri.EscapeDataString(oauth_consumer_secret),
                                "&", Uri.EscapeDataString(oauth_token_secret));

        string oauth_signature;
        using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(compositeKey)))
        {
            oauth_signature = Convert.ToBase64String(
                hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(baseString)));
        }

        // create the request header
        var headerFormat = "OAuth oauth_nonce=\"{0}\", oauth_signature_method=\"{1}\", " +
                           "oauth_timestamp=\"{2}\", oauth_consumer_key=\"{3}\", " +
                           "oauth_token=\"{4}\", oauth_signature=\"{5}\", " +
                           "oauth_version=\"{6}\"";

        var authHeader = string.Format(headerFormat,
                                Uri.EscapeDataString(oauth_nonce),
                                Uri.EscapeDataString(oauth_signature_method),
                                Uri.EscapeDataString(oauth_timestamp),
                                Uri.EscapeDataString(oauth_consumer_key),
                                Uri.EscapeDataString(oauth_token),
                                Uri.EscapeDataString(oauth_signature),
                                Uri.EscapeDataString(oauth_version)
                        );


        // make the request

        ServicePointManager.Expect100Continue = false;

        var postBody = "screen_name=" + Uri.EscapeDataString(screen_name);//
        resource_url += "?" + postBody;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(resource_url);
        request.Headers.Add("Authorization", authHeader);
        request.Method = "GET";
        request.ContentType = "application/x-www-form-urlencoded";


        WebResponse response = request.GetResponse();
        return new StreamReader(response.GetResponseStream()).ReadToEnd();
    }

    public static string getResponce(HttpWebRequest request)
    {
        try
        {
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(resp.GetResponseStream());
            string result = reader.ReadToEnd();
            reader.Close();
            return result + "&status=200";
        }
        catch (Exception ex)
        {
            string statusCode = "";
            if (ex.Message.Contains("403"))
                statusCode = "403";
            else if (ex.Message.Contains("401"))
                statusCode = "401";
            return string.Format("status={0}&error={1}", statusCode, ex.Message);
        }
    }
     
    #endregion
}

public class OAuthUtility
{
    public OAuthUtility()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    #region *******Common Methods**********
    protected static string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";
    public static string UrlEncode(string value)
    {
        StringBuilder result = new StringBuilder();

        foreach (char symbol in value)
        {
            if (unreservedChars.IndexOf(symbol) != -1)
            {
                result.Append(symbol);
            }
            else
            {
                result.Append('%' + String.Format("{0:X2}", (int)symbol));
            }
        }

        return result.ToString();
    }

    public static string GenerateTimeStamp()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds).ToString();
    }
    public static string GenerateNonce()
    {
        // Just a simple implementation of a random number between 123400 and 9999999
        Random random = new Random();
        return random.Next(123400, 9999999).ToString();
    }
    public static Dictionary<string, string> GetQueryParameters(string dataWithQuery)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();
        string[] parts = dataWithQuery.Split('?');
        if (parts.Length > 0)
        {
            string QueryParameter = parts.Length > 1 ? parts[1] : parts[0];
            if (!string.IsNullOrEmpty(QueryParameter))
            {
                string[] p = QueryParameter.Split('&');
                foreach (string s in p)
                {
                    if (s.IndexOf('=') > -1)
                    {
                        string[] temp = s.Split('=');
                        result.Add(temp[0], temp[1]);
                    }
                    else
                    {
                        result.Add(s, string.Empty);
                    }
                }
            }
        }
        return result;
    }
    #endregion Common Methods

    public static string GetAuthorizationHeaderForPost_OR_QueryParameterForGET(Uri url, string callbackUrl, string httpMethod, string consumerKey, string consumerSecret, string token, string tokenSecret, out string normalizedUrl)
    {
        string normalizedParameters = "";

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("oauth_version", "1.0");
        if (token != "")
            parameters.Add("oauth_token", token);
        parameters.Add("oauth_nonce", GenerateNonce()); //Random String
        parameters.Add("oauth_timestamp", GenerateTimeStamp()); // Current Time Span
        parameters.Add("oauth_consumer_key", consumerKey); //Customer Consumer Key
        parameters.Add("oauth_signature_method", "HMAC-SHA1"); //Singnatur Encription Method
        parameters.Add("oauth_callback", UrlEncode(callbackUrl)); //return url

        Dictionary<string, string> drQuery = GetQueryParameters(url.Query);
        foreach (string key in drQuery.Keys)
            parameters.Add(key, drQuery[key]);

        if (url.Query != "")
            normalizedUrl = url.AbsoluteUri.Replace(url.Query, "");
        else
            normalizedUrl = url.AbsoluteUri;

        List<string> li = parameters.Keys.ToList();
        li.Sort();

        StringBuilder sbOAuthHeader = new StringBuilder("OAuth ");
        StringBuilder sbSignatureBase = new StringBuilder();
        foreach (string k in li)
        {
            sbSignatureBase.AppendFormat("{0}={1}&", k, parameters[k]); // For Signature and Get Date (QueryString)
            sbOAuthHeader.AppendFormat("{0}=\"{1}\", ", k, parameters[k]); // For Post Request (Post Data)
        }

        string signature = GenerateSignatureBySignatureBase(httpMethod, consumerSecret, tokenSecret, normalizedUrl, sbSignatureBase);

        if (httpMethod == "POST")
        {
            string OAuthHeader = sbOAuthHeader.Append("oauth_signature=\"" + UrlEncode(signature) + "\"").ToString();
            normalizedParameters = OAuthHeader;
        }
        else if (httpMethod == "GET")
        {
            normalizedParameters = sbSignatureBase.AppendFormat("{0}={1}", "oauth_signature", signature).ToString(); ;
        }
        return normalizedParameters;
    }
    private static string GenerateSignatureBySignatureBase(string httpMethod, string consumerSecret, string tokenSecret, string normalizedUrl, StringBuilder sbSignatureBase)
    {
        string normalizedRequestParameters = sbSignatureBase.ToString().TrimEnd('&');
        StringBuilder signatureBase = new StringBuilder();
        signatureBase.AppendFormat("{0}&", httpMethod.ToString());
        signatureBase.AppendFormat("{0}&", UrlEncode(normalizedUrl));
        signatureBase.AppendFormat("{0}", UrlEncode(normalizedRequestParameters));

        HMACSHA1 hmacsha1 = new HMACSHA1();
        hmacsha1.Key = Encoding.ASCII.GetBytes(string.Format("{0}&{1}", UrlEncode(consumerSecret), UrlEncode(tokenSecret)));
        byte[] hashBytes = hmacsha1.ComputeHash(System.Text.Encoding.ASCII.GetBytes(signatureBase.ToString()));
        return Convert.ToBase64String(hashBytes);
    }
}


