<%@ Page Title="" Language="C#" MasterPageFile="AdminMaster.Master" AutoEventWireup="true" CodeBehind="ViewImages.aspx.cs" Inherits="MakeNMake.ServiceEngineer.ViewImages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Static/bootstrap/bootstrap.min.css" rel="stylesheet" />
    <link href="../ImageGallery/blueimp-gallery.min.css" rel="stylesheet" />
    <link href="../ImageGallery/bootstrap-image-gallery.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title paneltitle">Images Related to Ticket</h3>
        </div>
        <div class="panel-body">
            <div class="col-sm-12">
                <div class="col-sm-12">
                    <div id="links">
                        <asp:Repeater ID="rptImages" runat="server" OnItemCommand="rptImages_ItemCommand">
                            <ItemTemplate>
                                <div style="width:105px;height:130px;margin:10px;float:left;border:1px black solid; ">
                              <center>
                                    <div style="width:100px;height:100px;">
                                    <asp:HyperLink ID="hyperMainImg" NavigateUrl='<%# string.Format("~/UserImages/Issues/{0}",Eval("ImageUrl")) %>' data-gallery="" runat="server">
                                        <asp:Image ID="imgThumb" Height="100" Width="100" ImageUrl='<%# string.Format("~/UserImages/IssuesThumb/{0}", Eval("ThumnailUrl")) %>' runat="server" />
                                    </asp:HyperLink>
                                </div>                                    
                                <asp:LinkButton ID="lnkBtn" CommandName="deleteImage" CommandArgument='<%#Eval("tblImageID") %>' runat="server">Delete</asp:LinkButton>
                               
                              </center>
                                     </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
                <div class="col-sm-12">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-success" OnClick="btnCancel_Click"  />
                </div>
            </div>
            <div id="blueimp-gallery" class="blueimp-gallery">
    <!-- The container for the modal slides -->
    <div class="slides"></div>
    <!-- Controls for the borderless lightbox -->
    <h3 class="title"></h3>
    <a class="prev">‹</a>
    <a class="next">›</a>
    <a class="close">×</a>
    <a class="play-pause"></a>
    <ol class="indicator"></ol>
    <!-- The modal dialog, which will be used to wrap the lightbox content -->
    <div class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" aria-hidden="true">&times;</button>
                    <h4 class="modal-title"></h4>
                </div>
                <div class="modal-body next"></div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default pull-left prev">
                        <i class="glyphicon glyphicon-chevron-left"></i>
                        Previous
                    </button>
                    <button type="button" class="btn btn-primary next">
                        Next
                        <i class="glyphicon glyphicon-chevron-right"></i>
                    </button>
                </div>
            </div>
        </div>
    </div>
</div>
        </div>
    </div>
    <script type="text/javascript" src="../Scripts/jquery-2.0.1.min.js"></script>
<script type="text/javascript" src="../Static/bootstrap/bootstrap.min.js"></script>
<script  type="text/javascript" src="../ImageGallery/bootstrap-image-gallery.min.js"></script>
<script type="text/javascript" src="../ImageGallery/jquery.blueimp-gallery.min.js"></script>
</asp:Content>
