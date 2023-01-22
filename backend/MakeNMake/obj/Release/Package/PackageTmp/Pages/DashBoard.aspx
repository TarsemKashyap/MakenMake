<%@ Page Title=" DashBoard" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="DashBoard.aspx.cs" Inherits="MakeNMake.Admin.DashBoard" %>

<%@ Register Src="~/UserControl/UpdateUserInfo.ascx" TagPrefix="uc1" TagName="UpdateUserInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/jquery-2.0.1.min.js"></script>
    <link href="../Static/bootstrap/bootstrap.css" rel="stylesheet" />
    <script src="../Static/bootstrap/bootstrap.min.js"></script>
    <script src="../Static/js/validation.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($("#hdnIsUpdated").val() == "1") {
                $('#myModal').modal({ show: true });
            }
            else {
                $('#myModal').modal('hide');
            }
        });
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                if ($("#hdnIsUpdated").val() == "1") {
                    $('#myModal').modal({ show: true });
                }
                else {
                    $('#myModal').modal('hide');
                }
            }
        }
        function ConfirmUser(data) {
            var getValue = confirm('Please Confirm the Customer by asking this Info :- \n' + data.Replace("&", "\n"));
            if (getValue) {
                window.location.href = "DashBoard.aspx";
            }
            else {
                window.location.href = "../Signup.aspx";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-lg-12">
        <ul>
            <li><a href="#">
                <img id="my-img" src="../Static/images/dashboard1.png" class="img-responsive" alt="Responsive image" onmouseover="this.src='../Static/images/dashboard2.png'" onmouseout="this.src='../Static/images/dashboard1.png'"></a></li>

            <li class="active"><a href="#">
                <img src="../Static/images/customerlist1.png" onmouseover="this.src='../Static/images/customerlist2.png'" onmouseout="this.src='../Static/images/customerlist1.png'" border="0" alt="" /></a></li>

            <li class="active"><a href="#">
                <img src="../Static/images/appointments1.png" onmouseover="this.src='../Static/images/appointments2.png'" onmouseout="this.src='../Static/images/appointments1.png'" border="0" alt="" /></a></li>

            <li class="active"><a href="#">
                <img src="../Static/images/worklist1.png" onmouseover="this.src='../Static/images/worklist2.png'" onmouseout="this.src='../Static/images/worklist1.png'" border="0" alt="" /></a></li>

            <li class="active"><a href="#">
                <img src="../Static/images/practise1.png" onmouseover="this.src='../Static/images/practise2.png'" onmouseout="this.src='../Static/images/practise1.png'" border="0" alt="" /></a></li>

            <li class="active"><a href="#">
                <img src="../Static/images/account1.png" onmouseover="this.src='../Static/images/account2.png'" onmouseout="this.src='../Static/images/account1.png'" border="0" alt="" /></a></li>
        </ul>
    </div>


         <div id="slideShow" runat="server" class="col-lg-12 slide" style="">
							<div id="slider1_container" style="display: none; position: relative; margin: 0 auto; width: 1140px; height: 442px; overflow: hidden;">
									<div u="loading" style="position: absolute; top: 0px; left: 0px;">
										<div style="filter: alpha(opacity=70); opacity:0.7; position: absolute; display: block;
						
										background-color: #000; top: 0px; left: 0px;width: 100%; height:100%;">
										</div>
										<div style="position: absolute; display: block; background: url(../Static/images/wait.gif) no-repeat center center;
						
										top: 0px; left: 0px;width: 100%;height:100%;">
										</div>
									</div>
						
									<div u="slides" style="cursor: move; position: absolute; left: 0px; top: 0px; width: 1140px; height: 442px;
									overflow: hidden;">
										<div>
											<img u="image" src="../Static/Dashboard_Banner/Dashboard_Banner.jpg" />
										</div>
										<div>
											<img u="image" src="../Static/Dashboard_Banner/Dashboard_Banner_s2.jpg" />
										</div>
										<div>
											<img u="image" src="../Static/Dashboard_Banner/Dashboard_Banner_s3.jpg" />
										</div>
										<div>
											<img u="image" src="../Static/Dashboard_Banner/Dashboard_Banner_s4.jpg" />
										</div>
										<div>
											<img u="image" src="../Static/Dashboard_Banner/Dashboard_Banner_s5.jpg" />
										</div>
										<div>
											<img u="image" src="../Static/Dashboard_Banner/Dashboard_Banner_s6.jpg" />
										</div>
										<div>
											<img u="image" src="../Static/Dashboard_Banner/Dashboard_Banner_s7.jpg" />
										</div>
									</div>
									<style>
										.jssorb05 {
											position: absolute;
										}
										.jssorb05 div, .jssorb05 div:hover, .jssorb05 .av {
											position: absolute;
											width: 16px;
											height: 16px;
											/*background: url(../img/b05.png) no-repeat;*/
											overflow: hidden;
											cursor: pointer;
										}
										.jssorb05 div { background-position: -7px -7px; }
										.jssorb05 div:hover, .jssorb05 .av:hover { background-position: -37px -7px; }
										.jssorb05 .av { background-position: -67px -7px; }
										.jssorb05 .dn, .jssorb05 .dn:hover { background-position: -97px -7px; }
									</style>
									<div u="navigator" class="jssorb05" style="bottom: 16px; right: 6px;">
										<div u="prototype"></div>
									</div>
									<style>
										.jssora11l, .jssora11r {
											display: block;
											position: absolute;
											/* size of arrow element */
											width: 37px;
											height: 37px;
											cursor: pointer;
											/*background: url(../img/a11.png) no-repeat;*/
											overflow: hidden;
										}
										.jssora11l { background-position: -11px -41px; }
										.jssora11r { background-position: -71px -41px; }
										.jssora11l:hover { background-position: -131px -41px; }
										.jssora11r:hover { background-position: -191px -41px; }
										.jssora11l.jssora11ldn { background-position: -251px -41px; }
										.jssora11r.jssora11rdn { background-position: -311px -41px; }
									</style>
									<!-- Arrow Left -->
									<span u="arrowleft" class="jssora11l" style="top: 123px; left: 8px;">
									</span>
									<!-- Arrow Right -->
									<span u="arrowright" class="jssora11r" style="top: 123px; right: 8px;">
									</span>
						  </div>
						</div>
    

  
    <asp:HiddenField ID="hdnIsUpdated" ClientIDMode="Static" runat="server" />
    <div class="modal fade" id="myModal" data-backdrop="static" tabindex="-1" role="grid"
        aria-labelledby="myModalLabel" aria-hidden="true">
        <uc1:UpdateUserInfo runat="server" ID="UpdateUserInfo" />
    </div>
    <script src="../Static/cssinner/slide/docs.min.js"></script>
        <script src="../Static/cssinner/slide/ie10-viewport-bug-workaround.js"></script>
        <script src="../Static/js/jssor.slider.mini.js"></script>
    <script type="text/javascript">

        jQuery(document).ready(function ($) {
            var options = {
                $AutoPlay: true,                                    //[Optional] Whether to auto play, to enable slideshow, this option must be set to true, default value is false
                $AutoPlaySteps: 1,                                  //[Optional] Steps to go for each navigation request (this options applys only when slideshow disabled), the default value is 1
                $AutoPlayInterval: 2000,                            //[Optional] Interval (in milliseconds) to go for next slide since the previous stopped if the slider is auto playing, default value is 3000
                $PauseOnHover: 1,                                   //[Optional] Whether to pause when mouse over if a slider is auto playing, 0 no pause, 1 pause for desktop, 2 pause for touch device, 3 pause for desktop and touch device, 4 freeze for desktop, 8 freeze for touch device, 12 freeze for desktop and touch device, default value is 1

                $ArrowKeyNavigation: true,   			            //[Optional] Allows keyboard (arrow key) navigation or not, default value is false
                $SlideEasing: $JssorEasing$.$EaseOutQuint,          //[Optional] Specifies easing for right to left animation, default value is $JssorEasing$.$EaseOutQuad
                $SlideDuration: 800,                                //[Optional] Specifies default duration (swipe) for slide in milliseconds, default value is 500
                $MinDragOffsetToSlide: 20,                          //[Optional] Minimum drag offset to trigger slide , default value is 20
                //$SlideWidth: 600,                                 //[Optional] Width of every slide in pixels, default value is width of 'slides' container
                //$SlideHeight: 300,                                //[Optional] Height of every slide in pixels, default value is height of 'slides' container
                $SlideSpacing: 0, 					                //[Optional] Space between each slide in pixels, default value is 0
                $DisplayPieces: 1,                                  //[Optional] Number of pieces to display (the slideshow would be disabled if the value is set to greater than 1), the default value is 1
                $ParkingPosition: 0,                                //[Optional] The offset position to park slide (this options applys only when slideshow disabled), default value is 0.
                $UISearchMode: 1,                                   //[Optional] The way (0 parellel, 1 recursive, default value is 1) to search UI components (slides container, loading screen, navigator container, arrow navigator container, thumbnail navigator container etc).
                $PlayOrientation: 1,                                //[Optional] Orientation to play slide (for auto play, navigation), 1 horizental, 2 vertical, 5 horizental reverse, 6 vertical reverse, default value is 1
                $DragOrientation: 1,                                //[Optional] Orientation to drag slide, 0 no drag, 1 horizental, 2 vertical, 3 either, default value is 1 (Note that the $DragOrientation should be the same as $PlayOrientation when $DisplayPieces is greater than 1, or parking position is not 0)

                $ArrowNavigatorOptions: {                           //[Optional] Options to specify and enable arrow navigator or not
                    $Class: $JssorArrowNavigator$,                  //[Requried] Class to create arrow navigator instance
                    $ChanceToShow: 2,                               //[Required] 0 Never, 1 Mouse Over, 2 Always
                    $AutoCenter: 2,                                 //[Optional] Auto center arrows in parent container, 0 No, 1 Horizontal, 2 Vertical, 3 Both, default value is 0
                    $Steps: 1,                                      //[Optional] Steps to go for each navigation request, default value is 1
                    $Scale: false                                   //Scales bullets navigator or not while slider scale
                },

                $BulletNavigatorOptions: {                                //[Optional] Options to specify and enable navigator or not
                    $Class: $JssorBulletNavigator$,                       //[Required] Class to create navigator instance
                    $ChanceToShow: 2,                               //[Required] 0 Never, 1 Mouse Over, 2 Always
                    $AutoCenter: 1,                                 //[Optional] Auto center navigator in parent container, 0 None, 1 Horizontal, 2 Vertical, 3 Both, default value is 0
                    $Steps: 1,                                      //[Optional] Steps to go for each navigation request, default value is 1
                    $Lanes: 1,                                      //[Optional] Specify lanes to arrange items, default value is 1
                    $SpacingX: 12,                                   //[Optional] Horizontal space between each item in pixel, default value is 0
                    $SpacingY: 4,                                   //[Optional] Vertical space between each item in pixel, default value is 0
                    $Orientation: 1,                                //[Optional] The orientation of the navigator, 1 horizontal, 2 vertical, default value is 1
                    $Scale: false                                   //Scales bullets navigator or not while slider scale
                }
            };

            $("#slider1_container").css("display", "block");
            var jssor_slider1 = new $JssorSlider$("slider1_container", options);
            function ScaleSlider() {
                var parentWidth = jssor_slider1.$Elmt.parentNode.clientWidth;
                if (parentWidth) {
                    jssor_slider1.$ScaleWidth(parentWidth - 30);
                }
                else
                    window.setTimeout(ScaleSlider, 30);
            }
            ScaleSlider();

            $(window).bind("load", ScaleSlider);
            $(window).bind("resize", ScaleSlider);
            $(window).bind("orientationchange", ScaleSlider);
            //responsive code end
        });
    </script>
</asp:Content>
