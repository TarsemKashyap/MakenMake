<%@ Page Title="Register | Make N Make" Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" MasterPageFile="~/SiteMaster.Master" MaintainScrollPositionOnPostback="true" Inherits="MakeNMake.SignUp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="Scripts/jquery-2.0.1.min.js"></script>
    <script src="Static/js/validation.js"></script>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7; IE=EmulateIE9" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1.0, user-scalable=no" />
    <link rel="stylesheet" type="text/css" href="makenmakeNew/style.css" media="all" />

    <link rel="stylesheet" type="text/css" href="makenmakeNew/demo.css" media="all" />
    <link href="Static/bootstrap/bootstrap.min.css" rel="stylesheet" />
    <script src="Static/bootstrap/bootstrap.min.js"></script>
    
    <style type="text/css">

        .form .gender {
              width: 290px;
        }


        .form .select-style {
            /*-webkit-padding-start: 48px;*/
                text-indent: 48px;
                  height: 25px;
  
    background-repeat: no-repeat;
    background-position: left;
        }
        .row
        {
            margin-left: -15px;
    margin-right: -15px;
        }
        .modal-body p {
            padding: 4px;
        }
        * {
            box-sizing: inherit;
        }
        .content-wrapper {
   
    min-height: 100%;
    z-index: 800;
}
.content-wrapper{
    margin-left:auto;
    transition: transform 0.3s ease-in-out 0s, margin 0.3s ease-in-out 0s;
    z-index: 820;
}
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
         
            $("#chkTerms").click(function () {
                //if ($("#chkTerms").is(":checked")) {
                //    //TermsCondition();
                //}
            });
            $("#chktermsref").click(function () {
                    TermsCondition();
            });
            //$("#chktermsref").mouseover(function () {
            //    if ($("#chkTerms").is(":checked")) {
            //        $('#myModal').modal('show');
            //    }
            //}); 

            

        });


        var typingTimer;                //timer identifier
        var doneTypingInterval = 2500;  //time in ms, 5 second for example
        

        //on keyup, start the countdown
        function myKeyUpFunction () {
            clearTimeout(typingTimer);
            typingTimer = setTimeout(doneTyping, doneTypingInterval);
        }

        //on keydown, clear the countdown 
        function myKeyDownFunction() {
            clearTimeout(typingTimer);
        }

        //user is "finished typing," do something
        function doneTyping() {
            isValidDOB();
        }


        function isValidDOB() {
            var day = Number($('#ContentPlaceHolder1_txtday')[0].value);
            var dayField = $('#ContentPlaceHolder1_txtday')[0];

            var month = Number($('#ContentPlaceHolder1_Ddlm')[0].value);
           
            var year = Number($('#ContentPlaceHolder1_txtDob')[0].value);
            var yearField = $('#ContentPlaceHolder1_txtDob')[0];

            var toDate = new Date();
            var currentYear =toDate.getFullYear();
            var currentMonth = (toDate.getMonth()) + 1;

            // checking valid days
            if (day != 0) {
                if (day < 1 || day > 31) {
                    alert("Day must be between 1 and 31");
                    dayField.value = "";
                    return false;
                }
            }

            //checking valid days
            if (month != 0) {

                if ((month == 4 || month == 6 || month == 9 || month == 11) && day == 31) {
                    alert("Month " + month + " doesn't have 31 days!")
                    dayField.value = "";
                    return false;
                }
                if (year != 0 ) {
                    if (year > 1900 && year < currentYear) {
                       if (month == 2) { // check for february 29th
                        var isleap = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));
                        if (day > 29 || (day == 29 && !isleap)) {
                            alert("February " + year + " doesn't have " + day + " days!");
                            dayField.value = "";
                            return false;
                        }
                    }
                }
                else {
                        alert("Please enter a valid year!");
                        yearField.value = "";
                        return false;
                }

                }
                
               
            }

            if (month == 0 && year != 0) {
                alert("Please select Month first.");
                yearField.value = "";
                return false;
            }
           
            return true;
        }
        function TermsCondition() {
            var text = $("#ddlPlans").val();
            var rdbvalue = "";
           
            var radioButtons = document.getElementsByName("<%=rdbServices.UniqueID%>");
            for (var x = 0; x < radioButtons.length; x++) {
                if (radioButtons[x].checked) {
                    rdbvalue =radioButtons[x].value;
                }
            }
            
            if (text == "F") {
                testwindow = window.open("TermsConditions_Flexi.aspx", "mywindow", "location=1,status=1,scrollbars=1,width=500,height=600");
                testwindow.moveTo(500, 0);
            }
            else if (text == "U" && rdbvalue == "Commercial") {
                testwindow = window.open("TermsConditions_Unlimited.aspx", "mywindow", "location=1,status=1,scrollbars=1,width=500,height=600");
                testwindow.moveTo(500, 0);
            }
            else if (text == "U" && rdbvalue == "Domestic") {
                testwindow = window.open("TermsCpnditionsUnlimitedDomestic.aspx", "mywindow", "location=1,status=1,scrollbars=1,width=500,height=600");
                testwindow.moveTo(500, 0);
            }
            else if (text == "M") {
                testwindow = window.open("TermsCondition_MYP.aspx", "mywindow", "location=1,status=1,scrollbars=1,width=500,height=600");
                testwindow.moveTo(500, 0);
            }
            else {
                $("#chkTerms").attr('checked', false);
                alert("Please select plan");
            }
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $(document).bind('contextmenu', function (e) {
                e.preventDefault();
                alert('Right Click is not allowed');
            });
        });
        function checkDate(sender, args) {
            //if (sender._selectedDate > new Date()) {
            //    alert("You cannot select a future date!");
            //    sender._selectedDate = new Date();
            //    // set the date back to the current date
            //    sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            //}
            var selectedDate = sender._selectedDate;

            var todayDate = new Date().format("dd/MM/yyyy");
            if (selectedDate > todayDate) {

                alert("You cannot select a future date!");

                // set the date back to the current date
                //     sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
            else if (todayDate.getFullYear() - selectedDate.getFullYear() < 18) {

                alert("You must be 18 year older to use this application");
            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
   <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" DynamicLayout="true" runat="server">
                <ProgressTemplate>
                    <div class="divWaiting" style="height:1500px!important;">
                        <img src="Static/images/wait.gif" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:Panel ID="divAdduser" runat="server" >
            
            <div class="content-wrapper">
                  <div class="row" style="background-image: url('makenmakeNew/oie_transparent.png');
    background-repeat: no-repeat;
    background-attachment: fixed;background-size:500px;
background-position:left;">
              
               
             

                   <div class="col-lg-4">
                    
                  <%--  <img src="makenmakeNew/oie_transparent.png" style="width:400px;padding-top:100px" />--%>
                </div>
                   <div class="col-lg-4">
                    <div class="logo" style="margin-left:auto">
                    <div class="img">
                        <img src="makenmakeNew/logo-new.png" style="height:104px;width:140px;"/>
                    </div>
                </div>
                
                       <div class="form" style="margin-left:auto;padding-left:0px !important;margin-top: -33px;">  
                 <%--  <div class="col-lg-10" style="height:30px;align-items:center;"></div>--%>
                             <!--<img src="images/22.png" style="padding-right:30px;">-->
                        <h2 style="font-size: 18px;font-weight: 400;padding-top:40px;text-align:center;  font-family: 'Lato', sans-serif;"> Register </h2>
                           <br />
                    <div id="contactform">
                        <p class="contact">
                            <label for="name">Name</label>
                        </p>
                        <asp:TextBox ID="txtfirstName" placeholder="First name" type="text" onkeypress="return ValidateCharacters(event)"
                            AutoCompleteType="None" ClientIDMode="Static" autocomplete="off" runat="server" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter First Name" ValidationGroup="signup"
                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtfirstName"></asp:RequiredFieldValidator>
                        <br /><br />
                        <p class="contact">
                            <label for="name">Last Name(optional)</label>
                        </p>
                        <asp:TextBox ID="txtlastName" ClientIDMode="Static" type="text" placeholder="Enter Last Name" AutoCompleteType="None" onkeypress="return ValidateCharacters(event)" autocomplete="off" runat="server" class=""></asp:TextBox>

                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter Last Name" ValidationGroup="signup"
                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtlastName"></asp:RequiredFieldValidator>
                        --%><br /><br />
                        <p class="contact">
                            <label for="email">Email</label>
                        </p>
                        <asp:TextBox ID="txtsignupid" ClientIDMode="Static" placeholder="Enter Email ID"
                            runat="server" class=""></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter EmailID" ValidationGroup="signup"
                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtsignupid"></asp:RequiredFieldValidator>

                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter Valid EmailID"
                            Display="Dynamic" ForeColor="Red" ControlToValidate="txtsignupid" ValidationGroup="signup"
                            ValidationExpression="\b[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}\b"></asp:RegularExpressionValidator>
                        <br /><br />
                        <p class="contact">
                            <label for="password">Password</label>
                        </p>
                        <asp:TextBox ID="txtpass" runat="server" AutoCompleteType="None" autocomplete="off"
                            placeholder="Enter Password Here!" TextMode="Password" class=""></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Password"
                            ControlToValidate="txtpass" Display="Dynamic" ForeColor="Red" ValidationGroup="signup"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="Regex2" runat="server" Display="Dynamic" ForeColor="Red" ControlToValidate="txtpass" ValidationGroup="signup"
                            ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{4,}$" ErrorMessage="*"
                             />
                        <br /><br />
                        <p class="contact">
                            <label for="repassword">Confirm Your Password</label>
                        </p>
                        <asp:TextBox ID="txtcfpwd" EnableTheming="false" autocomplete="off" runat="server"
                            placeholder="Enter Confirm Password Here!" TextMode="Password" class=""></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Enter Confirm Password"
                            ControlToValidate="txtcfpwd" Display="Dynamic" ForeColor="Red" ValidationGroup="signup"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1"
                            runat="server"
                            ControlToCompare="txtpass"
                            ControlToValidate="txtcfpwd"
                            Operator="Equal" Display="Dynamic" ForeColor="Red"
                            ErrorMessage="Password & Confirm Password Should be Same"
                            ValidationGroup="signup" SetFocusOnError="True">                       
                        </asp:CompareValidator>
                        <br /><br />
                        <p class="contact">
                            <label for="phone">Mobile Phone</label>
                        </p>
                        <asp:TextBox ID="txtmobile" ClientIDMode="Static" placeholder="Enter Mobile Number" onkeypress="return ValidateNumber(event)"
                            MaxLength="10" AutoCompleteType="None" runat="server" class=""></asp:TextBox>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Enter Mobile Number" ValidationGroup="signup"
                            ForeColor="Red" Display="Dynamic" ControlToValidate="txtmobile"></asp:RequiredFieldValidator>
                        <br /><br />
                        <p class="contact">
                            <label for="phone">Gender(optional)
</label>
                        </p>
                        <asp:DropDownList ID="ddlGender" class="select-style gender" runat="server">
                            <asp:ListItem Text=" I am.. " Value="0"></asp:ListItem>
                            <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                            <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                        </asp:DropDownList>
                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Enter Gender" ValidationGroup="signup"
                            ForeColor="Red" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ControlToValidate="ddlGender"></asp:RequiredFieldValidator>--%>

                        <br />
                        <br />


                        <p class="contact">
                            <label for="repassword">Birthday</label>
                        </p>
                                           <%--<fieldset>
                           
                                               <label class="month">
                                               <asp:DropDownList ID="ddlmonth" runat="server">
                                                   <asp:ListItem Text="Month" Value="0"></asp:ListItem>
                                                   <asp:ListItem Text="Jaunary" Value="1"></asp:ListItem>
                                                   <asp:ListItem Text="Febuary" Value="2"></asp:ListItem>
                                                   <asp:ListItem Text=" March " Value="3"></asp:ListItem>
                                                   <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                                   <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                                   <asp:ListItem Text=" June " Value="6"></asp:ListItem>
                                                   <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                                   <asp:ListItem Text="August" Value="8"></asp:ListItem>
                                                   <asp:ListItem Text=" September" Value="9"></asp:ListItem>
                                                   <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                                   <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                                   <asp:ListItem Text="December" Value="11"></asp:ListItem>
                                               </asp:DropDownList>
                                               </label>
                                               
                                               <label>
                                               Day<asp:TextBox ID="txtDay" runat="server" Cssclass="birthday" maxlength="2" placeholder="Day" />
                                               </label>
                                               <label>
                                               Year
                                               <asp:TextBox ID="txtDob" runat="server" Cssclass="birthyear" maxlength="4" name="BirthYear" placeholder="Year" />
                                               </label>
                                               <br>
                                               <br></br>
                                               </br>
                              </fieldset>--%>
                        <asp:DropDownList ID="Ddlm" runat="server" CssClass="select-style month" style="text-indent:24px;" OnSelectedIndexChanged="Ddlm_SelectedIndexChanged" >
                                                   <asp:ListItem Text="Month" Value="0"></asp:ListItem>
                                                   <asp:ListItem Text="Jaunary" Value="01"></asp:ListItem>
                                                   <asp:ListItem Text="Febuary" Value="02"></asp:ListItem>
                                                   <asp:ListItem Text=" March " Value="03"></asp:ListItem>
                                                   <asp:ListItem Text="April" Value="04"></asp:ListItem>
                                                   <asp:ListItem Text="May" Value="05"></asp:ListItem>
                                                   <asp:ListItem Text=" June " Value="06"></asp:ListItem>
                                                   <asp:ListItem Text="July" Value="07"></asp:ListItem>
                                                   <asp:ListItem Text="August" Value="08"></asp:ListItem>
                                                   <asp:ListItem Text=" September" Value="09"></asp:ListItem>
                                                   <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                                   <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                                   <asp:ListItem Text="December" Value="11"></asp:ListItem>
                                               </asp:DropDownList>      
                        Day:<asp:TextBox ID="txtday" runat="server" Width="40px" MaxLength="2" placeholder="DD" onkeypress="return ValidateNumber(event)" />
                         <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txtday" ValidationGroup="signup" ID="RegularExpressionValidator2" ValidationExpression = "^[\s\S]{2,2}$" runat="server" ErrorMessage="Minimum 4 and Maximum 4 characters required."></asp:RegularExpressionValidator>
                         <asp:RangeValidator ID="RangeValidator1"
ControlToValidate="txtday"
MinimumValue="01"
MaximumValue="31"
Type="Integer"
EnableClientScript="false" ValidationGroup="signup"
Text="The day must be between 01 and 31"
runat="server" />
                      <%--  <asp:RegularExpressionValidator ID="RegularExpressionValidator3" SetFocusOnError="true" runat="server" ControlToValidate="txtday" 
                            ErrorMessage="Enter Day" Display="Dynamic" ForeColor="Red" ValidationGroup="signup" ValidationExpression="^(3[01]|[12][0-9]|[1-9])$"></asp:RegularExpressionValidator>
                        --%>
                        Year:<asp:TextBox ID="txtDob" runat="server" Width="68px" MaxLength="4" placeholder="YYYY" onkeypress="return ValidateNumber(event)" />
                        <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txtDob" ValidationGroup="signup" ID="RegularExpressionValidator3" ValidationExpression = "^[\s\S]{4,4}$" runat="server" ErrorMessage="Minimum 4 and Maximum 4 characters required."></asp:RegularExpressionValidator>
                        <asp:RangeValidator ID="RangeValidator2"
ControlToValidate="txtDob"
MinimumValue="1800" 
MaximumValue="2000"
Type="Integer" ValidationGroup="signup"
EnableClientScript="false"
Text="The year must be be below 1990"
runat="server" />
                        <%--<fieldset>
                        <asp:DropDownList ID="ddlmonth" runat="server">
                            <asp:ListItem Text=" Select Month" Value="00"></asp:ListItem>
                            <asp:ListItem Text="Jaunary" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Febuary" Value="2"></asp:ListItem>
                            <asp:ListItem Text=" March " Value="3"></asp:ListItem>
                            <asp:ListItem Text="April" Value="4"></asp:ListItem>
                            <asp:ListItem Text="May" Value="5"></asp:ListItem>
                            <asp:ListItem Text=" June " Value="6"></asp:ListItem>
                            <asp:ListItem Text="July" Value="7"></asp:ListItem>
                            <asp:ListItem Text="August" Value="8"></asp:ListItem>
                            <asp:ListItem Text=" September" Value="9"></asp:ListItem>
                            <asp:ListItem Text="October" Value="10"></asp:ListItem>
                            <asp:ListItem Text="November" Value="11"></asp:ListItem>
                            <asp:ListItem Text="December" Value="11"></asp:ListItem>
                        </asp:DropDownList>
                     
                       <label for="repassword">Day <asp:TextBox ID="txtDay" AutoComplete="off" runat="server" CssClass="birthday"></asp:TextBox></label>
                        <label for="repassword">Year <asp:TextBox ID="txtDob" AutoComplete="off" runat="server" CssClass="birthyear"></asp:TextBox></label>
                        </fieldset>--%>
                       <%-- <asp:CalendarExtender ID="CalendarExtender1" OnClientDateSelectionChanged="checkDate"
                            TargetControlID="txtDob" Format="dd/MM/yyyy" SelectedDate='<%#DateTime.Now %>' runat="server">
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10"
                            runat="server" ValidationGroup="signup" Display="Dynamic" ForeColor="Red"
                            ControlToValidate="txtDob" SetFocusOnError="true" ErrorMessage="Enter Birthday Date"></asp:RequiredFieldValidator>--%>
                       <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator3" SetFocusOnError="true" runat="server" ControlToValidate="txtDob"
                            ErrorMessage="Enter Valid Birthday Date" Display="Dynamic" ForeColor="Red" ValidationGroup="signup" ValidationExpression="^(((((0[1-9])|(1\d)|(2[0-8]))\/((0[1-9])|(1[0-2])))|((31\/((0[13578])|(1[02])))|((29|30)\/((0[1,3-9])|(1[0-2])))))\/((20[0-9][0-9])|(19[0-9][0-9])))|((29\/02\/(19|20)(([02468][048])|([13579][26]))))$"></asp:RegularExpressionValidator>
                        --%><br /><br />

                        <p class="contact">
                            <label for="address">Address(optional)</label>
                        </p>

                        <asp:TextBox ID="txtaddresslocality" ClientIDMode="Static" placeholder="House No./Flat No." Style="background-color:white;padding-left:20px;resize: none;width:290px;" AutoComplete="off" MaxLength="300"
                            CssClass=" " aria-describedby="basic-addon1" runat="server" ></asp:TextBox>
                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator11"
                            runat="server" ValidationGroup="signup" Display="Dynamic" ForeColor="Red"
                            ControlToValidate="txtaddresslocality" SetFocusOnError="true" ErrorMessage="Enter Your Locality"></asp:RequiredFieldValidator>--%>
                       <br /><br />
                        <asp:TextBox ID="txtstreet" ClientIDMode="Static" placeholder="Area/Locality/Building/Apartment" Style="background-color:white;padding-left:20px;resize: none;width:290px;" AutoComplete="off" MaxLength="300"
                            CssClass=" " aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator16"
                            runat="server" ValidationGroup="signup" Display="Dynamic" ForeColor="Red"
                            ControlToValidate="txtstreet" SetFocusOnError="true" ErrorMessage="Enter Your Street Address"></asp:RequiredFieldValidator>--%>
                        <br />
                      <%--  <p class="Country">
                            <label for="country">Country</label>
                        </p>--%>
                        <br />
                        <asp:DropDownList ID="ddlCountry" ClientIDMode="AutoID" style="background: white;" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                            CssClass="select-style country" runat="server" Width="290px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="signup" runat="server"
                            ErrorMessage="Enter Country" InitialValue="0"
                            ControlToValidate="ddlCountry" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <br />
                        <%--<p class="State">
                            <label for="state">State</label>
                        </p>--%>
                        <br />
                        <asp:DropDownList ID="ddlState" CssClass="select-style country" ClientIDMode="AutoID"  style="background:white" AutoPostBack="true" Width="290px" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ValidationGroup="signup" runat="server"
                            ErrorMessage="Enter State" InitialValue="0"
                            ControlToValidate="ddlState" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            
                        <br />
                        <br />
                       <%-- <p class="District">
                            <label for="district">District</label>
                        </p>--%>
                        <asp:DropDownList ID="ddlDistrict" ClientIDMode="AutoID" style="background:white" AutoPostBack="true" Width="290px"
                            OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" CssClass="select-style country" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ValidationGroup="signup" runat="server"
                            ErrorMessage="Enter District" InitialValue="0"
                            ControlToValidate="ddlDistrict" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator><br />
                        <br />
                       <%-- <p class="City">
                            <label for="city">City</label>
                        </p>--%>
                        <asp:DropDownList ID="ddlCity" ClientIDMode="Static" CssClass="select-style country" runat="server" Width="290px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ValidationGroup="signup" runat="server"
                            ErrorMessage="Enter City" InitialValue="0"
                            ControlToValidate="ddlCity" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator><br />
                        <br /><br />



                        <div class="demo3" style="padding-left: 31px; width: 85%;">
                            <div class="demo4" style="text-align: center; padding-top: 15px;">
                                <asp:RadioButtonList ID="rdbServices" CssClass=""  RepeatDirection="Horizontal" runat="server" Width="290px">
                                    <asp:ListItem Text="Domestic" style="margin-right: 20px"></asp:ListItem>
                                    <asp:ListItem Text="Commercial"></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Select Category"
                                    ControlToValidate="rdbServices" Display="Dynamic" ForeColor="Red"
                                    ValidationGroup="signup"></asp:RequiredFieldValidator>
                               
                            </div>
                        </div>
                        <br />
                        <br />

                        <asp:DropDownList ID="ddlPlans" ClientIDMode="Static" CssClass="select-style services" runat="server" Width="290px">
                            <asp:ListItem Text="--Select Service Plan--" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Unlimited" Value="U"></asp:ListItem>
                            <asp:ListItem Text="Make Your Plan" Value="M"></asp:ListItem>
                            <asp:ListItem Text="Flexi" Value="F"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Select Plan"
                            ControlToValidate="ddlPlans" InitialValue="0"
                            Display="Dynamic" ForeColor="Red" ValidationGroup="signup"></asp:RequiredFieldValidator>

                        <br />
                        <br />

                        <asp:CheckBox ID="chkTerms" ClientIDMode="Static" runat="server" />
                        <span>I agree to <a href="#" id ="chktermsref">terms and conditions </a> </span>
                        
                        <br />
                        <div class="demo" style="margin-top:20px;clear:both;">
                          
                            
                            
                              <asp:Button ID="btnSignup" runat="server" style="float:left;margin-left:25px;background-color:green;color:white;border-radius:10px;" ClientIDMode="Static" ValidationGroup="signup" Text="Register"
                                class="" OnClick="btnSignUp_Click"  />
                            <asp:Button ID="btnSignupPay" runat="server" style="float:left;margin-left:10px;background-color:green;color:white;border-radius:10px;" ClientIDMode="Static" ValidationGroup="signup" Text="Register & Pay"
                                class="" OnClick="btnSignupPay_Click" />
                            <asp:Button ID="btnCancel" runat="server" style="float:left;margin-left:10px;background-color:green;color:white;border-radius:10px;" Text="Cancel" OnClick="btnCancel_Click"
                                class="" />

                        </div>

                      
                    </div>
            </div>
                   </div>
                   <div class="col-lg-3">
                   <div class="row">
              <div class="col-lg-10" style="height:117px;"></div>
                      <%-- <%-- <div class="col-lg-10" style="height:50px;"></div>
                             <!--<img src="images/22.png" style="padding-right:30px;">-->
                        <h2 style="font-size:medium;font-weight:bold;text-align:left;">  OR
                            Login With Social Site
                        </h2>--%>
                        
                       
<%--<div class="image" style="text-align: left; clear:both; background-repeat: no-repeat;background-attachment: fixed;">--%>
                        <div class="image" style="text-align:center; padding-top:20px;position: fixed;right: 13%; top: 90px;">
				<h2 style="font-size: 18px;font-weight: 400; padding-right: 30px;font-family: 'Lato', sans-serif;">Or Register With Social Site</h2>
                            <asp:ImageButton ID="btn_fblogin" ImageUrl="~/makenmakeNew/2.png" runat="server" OnClick="btn_fblogin_Click" />
                           <%-- <img src="makenmakeNew/1.png" />--%>
                           <%-- <img src="makenmakeNew/1.png" />--%>
                            <asp:ImageButton ID="btnglogin" runat="server" ImageUrl="~/makenmakeNew/1.png" OnClick="btnglogin_Click"  />
                           <%-- <img src="makenmakeNew/3.png" />--%>
                            <asp:ImageButton ID="btnlinkdinlogin" runat="server" ImageUrl="~/makenmakeNew/3.png" OnClick="btnlinkdinlogin_Click"  />
                           <%-- <img src="makenmakeNew/4.png" />--%>
                               <asp:ImageButton ID="btn_twitterlogin" ImageUrl="~/makenmakeNew/4.png" runat="server" OnClick="btn_twitterlogin_Click" />
                            <div id="auth-status">
<div id="auth-loggedout">

            <div class="fb-login-button" autologoutlink="true" scope="email,user_checkins" ></div>
            </div>
<div id="auth-loggedin" style="display: none">
Hi, <span id="auth-displayname"></span>(<a href="#" id="auth-logoutlink">logout</a>)
</div>
</div>

                        </div>
                       
                       
                    </div>
                   </div>
               </div>
                
            </div>
               </asp:Panel>
            <asp:HiddenField ID="hdnUserID" runat="server" />
             <asp:HiddenField ID="hdverifycontent" runat="server" />
           
                     <asp:Panel id="dvVerifyOTP" CssClass="col-sm-12" runat="server" Visible="false" style="padding: 0;">
                           <div class="content-wrapper">
                 <div class="row">
              
               
             

                   <div class="col-lg-4">
                    
                  <%--  <img src="makenmakeNew/oie_transparent.png" style="width:400px;padding-top:100px" />--%>
                </div>
                   <div class="col-lg-4">
                    <div class="logo" style="margin-left:auto">
                    <div class="img">
                        <img src="makenmakeNew/logo-new.png" style="height:104px;width:140px;"/>
                    </div>
                </div>
               
                       <div class="form" style="margin-left:auto;padding-left:0px !important;">  
            <div id="contactform">
                        <div class="input-group input-group-sm">
                            <p>Enter OTP</p>
                            <asp:TextBox ID="txtOTP" AutoComplete="off"
                                placeholder="Enter OTP" MaxLength="4" CssClass="form-control" aria-describedby="basic-addon1" runat="server"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                            runat="server" ValidationGroup="otp" Display="Dynamic" ForeColor="Red"
                            ControlToValidate="txtOTP" SetFocusOnError="true" ErrorMessage="Enter OTP"></asp:RequiredFieldValidator>
                   
                   
                        <asp:Button ID="btnVerify" ValidationGroup="otp" style="margin:10px;background-color:green;color:white" runat="server" CssClass="btn btn-success" Text="Verify" OnClick="btnVerify_Click" />
                  
                 </div>  
                      </div></div>
                     </div>
                               </div>
                         

                         </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>    
</asp:Content>
