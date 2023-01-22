<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.Master" AutoEventWireup="true" CodeBehind="TermsCondition_MYP.aspx.cs" Inherits="MakeNMake.TermsCondition_MYP" %>

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
        .form .select-style {
            -webkit-padding-start: 48px;
        }

        .modal-body p {
            padding: 4px;
        }

        * {
            box-sizing: inherit;
        }
        .auto-style1 {
            margin: 0;
            line-height: 1.42857143;
            font-family: Garamond;
            color: #009999;
            text-decoration: underline;
        }
        .auto-style2 {
            font-family: Garamond;
            color: #009999;
        }
        .auto-style3 {
            font-family: Cambria;
            font-size: 11pt;
        }
        .auto-style4 {
            text-indent: -.25in;
            line-height: 105%;
            font-size: 11.0pt;
            font-family: Cambria, serif;
            margin-left: .5in;
            margin-right: 0in;
            margin-top: 0in;
            margin-bottom: .0001pt;
        }
        .auto-style5 {
            text-indent: -.25in;
            line-height: 105%;
            font-size: 11.0pt;
            font-family: Cambria, serif;
            margin-left: .5in;
            margin-right: 0in;
            margin-top: 0in;
            margin-bottom: 10.0pt;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="modal-content" style="width: auto; margin: 2px;">
        
        <div class="row header" style="padding-left: 20px; padding-right: 20px;">
            <div class="col-xs-12">
                <img src="Static/images/editlogo.png" style=" height: 200px;" class="logo" alt="" />
            </div>
            <%--<div class="col-xs-6 col-md-5 col-sm-5 col-lg-5 searchbox text-right">
                <div class="col-xs-12" style="font-size: 18px; font-weight: bold; text-align: left;">
                    MnM Services Pvt. Ltd.
                </div>
                <div class="col-xs-12" style="text-align: left; font-size: 12px;">
                    <span class="glyphicon glyphicon-home"></span>C-503 , UNITECH BUSINESS ZONE , NIRVANA COUNTRY,<br />
                    SECTOR 50 GURGAON-122018
                </div>
                <div class="col-xs-12" style="text-align: left; font-size: 12px;">
                    <span class="glyphicon glyphicon-home"></span>548 , BLOCK B , SECTOR 49 , FARIDABAD -121001  
                </div>
            </div>--%>
        </div>
        <div class="modal-header">

            <h3 class="auto-style1" id="myModalLabel">
                <b>Terms And Privacy Conditions(Make Your Plan)</b></h3>
        </div>
        <div class="modal-body" style="background: url('Static/images/watermarkedit.png') no-repeat;">
            <p>
                <span class="auto-style3">1.  Initial Response time shall be in between 0-3 working hrs for the complaints registered before 5 P.M. and resolution time depends upon nature of complaints.</p>
            <p>
                2.   Our scope of all services will be inside the radius of premises only.</p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;
                Outside and government liaisons area and other agency scope will not be included.</p>
            <p>
                &nbsp;&nbsp;<strong>&nbsp; Electrical Scope</strong>–Already installed DB/Switchboard /panels etc. scope provided that they should be inside the premises only.</p>
            <p>
                &nbsp;&nbsp;&nbsp; <strong>Plumbing Scope </strong>- Water Line and Sewerage lines inside the premises only.</p>
            <p>
                &nbsp;&nbsp;&nbsp; <strong>Air conditioning </strong>– includes Split,Window and Ductable AC’s only (excludes VRV units).</p>
            <p>
                &nbsp;&nbsp;&nbsp; <strong>Carpentry </strong>- includes repairing of the furniture not manufacturing.</p>
            <p>
                3.	Spare parts billing will be as per the consumption. Customer can provide their own spare parts.</p>
            <p>
                4.	Spares arrangement shall be done on SOS basis and depends upon their availability.</p>
            <p>
                5.	The repair estimate, spare rates shall be shared with customer for the approval before carrying out repairs.</p>
            <p>
                6.	Complains pending due to spare parts unavailability on customer’s end shall automatically be considered closed after 3 working days.</p>
            <p>
                7.	In case of any special/uncommon spare, the resolution time may increase depending upon the availability of the same.</p>
            <p>
                8.	Time duration for the single call is 90 minutes. In case, if time frame increases then second service call will automatically start.Travel time is not included</p>
            <p>
                9.	You can switch from Make Your Own Plan to Unlimited/A la Carte Plan or Vice Versa within 45 days of your plan start date. Amount shall be adjusted according to pro-data basis and customer needs to pay the due amount if any.</p>
            <p>
                10.	Basic items like ladder,cleaning clothes will be provided by customer.</p>
            <p>
                11.	The charges published are technician visit only.</p>
            <p>
                12.	Civil nature repairs i.e. mason work, digging, paint, plaster, brick work, labour work etc. shall not be in our scope. We can provide you an estimate for such issues.</p>
            <p>
                13.	The technician shall rectify the complaints for the existing set-up and shall not carry out new installation work. Any new installation work should be intimated by the client during call registration and we shall provide you a quote after the inspection.</p>
            <p>
                14.	Company shall not be responsible for any damage during the fault diagnosis and rectification.</p>
            <p>
                15.	Some major repairs may take more time and customer cooperation will be appreciated.</p>
            <p>
                16.	Add-on services are not part of the Make Your Own Plan and to be purchased separately.</p>
            <p>
                17.	The services under Make Your Plan includes Basic Services only i.e. Electrical, Plumbing, Carpentry, Air-conditioning.</p>
            <p>
                18.	Make Your Own Plan will automatically expire after tenure of 1 year and the remaining calls will expire automatically.</p>
            <p>
                19.	Client can only change the address up to “2” times during the tenure of the plan.</p>
            <p>
                20.	All disputes are subject to Gurgaon Jurisdiction.</span></p>
 

            <hr />
          <p>
                    <h3 class="auto-style2"> <b>Refund Policy: </b></h3>

            <p class="auto-style4" style="mso-add-space: auto; mso-list: l0 level1 lfo1">
                <![if !supportLists]><span lang="EN-IN">1.<span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span><![endif]><span lang="EN-IN">We shall have a refund policy of maximum 15 days. <o:p></o:p></span>
            </p>
            <p class="auto-style4" style="mso-add-space: auto; mso-list: l0 level1 lfo1">
                <![if !supportLists]><span lang="EN-IN">2.<span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span><![endif]><span lang="EN-IN">The customer shall not eligible for refund if “3” calls has already been consumed before 15 days of plan activation date.<o:p></o:p></span></p>
            <p class="auto-style5" style="mso-add-space: auto; mso-list: l0 level1 lfo1">
                <![if !supportLists]><span lang="EN-IN">3.<span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></span><![endif]><span lang="EN-IN">Company have the right to cancel the subscription of the customer at any time during the subscription and in that case pro-data amount shall be refunded.<o:p></o:p></span></p>
            <p class="MsoNormal">
                <span lang="EN-IN"><o:p>&nbsp;</o:p></span></p>
            <hr />
            <p class="MsoNormal">
                <span>You can register your complain through our Mobile App “<b>Make n Make</b>”/ Web site </span><a href="http://www.makenmake.in"><span>www.makenmake.in</span></a><span>, at our helpline number: <b>0124-4702600 ,0124-4315960 </b>or drop a mail at </span><a href="mailto:customercare@makenmake.co.in"><span>customercare@makenmake.in</span></a><span> between 9:00 A.M-5:30 P.M. (7 days a week</span></p>
            <p class="MsoNormal">
                <span class="auto-style5" style="text-underline: single;"><span>For escalation, drop a mail at </span></span><span><a href="mailto:escalation@makenmake.in">escalation@makenmake.in</a></span></p>
            <p class="MsoNormal">
                &nbsp;</p>
              
             

        </div>
        <div class="row header">

            <div class="col-xs-12">
                <img src="Static/images/editedfooter.png" style=" width:97%;height:110px" class="logo" alt="" />
            </div>
        </div>

    </div>
</asp:Content>
