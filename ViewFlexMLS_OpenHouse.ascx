<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewFlexMLS_OpenHouse.ascx.cs" Inherits="GIBS.Modules.FlexMLS_OpenHouse.ViewFlexMLS_OpenHouse" %>


<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<dnn:DnnCssInclude ID="DnnCssInclude1" runat="server" FilePath="~/DesktopModules/GIBS/FlexMLS_OpenHouse/css/StyleSheet.css" />
<dnn:DnnCssInclude ID="DnnCssInclude2" runat="server" FilePath="https://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/redmond/jquery-ui.css" />

<!-- fotorama.css & fotorama.js. -->
<link  href="https://cdnjs.cloudflare.com/ajax/libs/fotorama/4.6.4/fotorama.css" rel="stylesheet" /> <!-- 3 KB -->
<script src="https://cdnjs.cloudflare.com/ajax/libs/fotorama/4.6.4/fotorama.js" type="text/javascript"></script> <!-- 16 KB -->
<!-- 2. Add images to <div class="fotorama"></div>. -->

<asp:Label ID="lblDebug" runat="server" Text=""></asp:Label>

    <div class="townlinkdiv">
    <asp:Repeater ID="RepeaterTowns" runat="server" OnItemDataBound="RepeaterTowns_OnItemDataBound" >
    <ItemTemplate>
    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-sm btn-default townlink">HyperLink</asp:HyperLink>  
    </ItemTemplate>
    </asp:Repeater></div>


<h2><asp:Label ID="lblTown" runat="server" /></h2>

<asp:ListView ID="ListView1" runat="server" OnItemDataBound="ListView1_ItemDataBound">
    <ItemTemplate>
        
 <div style="display: inline-block; vertical-align:top; margin:6px; background-color:#fffee9;">
     <div style="margin: 0 auto !important;">

         <div class="fotorama" data-width="250" data-fit="cover" data-ratio="800/600">
<asp:HyperLink ID="HyperLinkImage"  runat="server"><asp:Image ID="imgListingImage" runat="server" ToolTip='<%# DataBinder.Eval(Container.DataItem,"Village")%> Open House' AlternateText='<%# DataBinder.Eval(Container.DataItem,"Village")%> Open House' /></asp:HyperLink>
</div>  

     </div>
     <div style="text-align: center; padding-bottom: 15px; width:250px;">

          <div class="ohhours">OPEN HOUSE<br /><%# DataBinder.Eval(Container.DataItem,"EventStart", "{0:D}").ToUpper() %><br /><%# DataBinder.Eval(Container.DataItem,"EventStart", "{0:h:mm tt}")%> to 
              <%# DataBinder.Eval(Container.DataItem,"EventEnd", "{0:h:mm tt}")%>
          </div>
         <div class="ohlistprice"><%# DataBinder.Eval(Container.DataItem,"ListingPrice", "{0:c0}")%></div>
         <div class="ohaddress"><asp:HyperLink ID="hyperlinkListingDetail" runat="server" CssClass="ohaddress" /></div>
         <div class="ohdetail"><asp:Label ID="lblDetails" runat="server" /> 
             <span runat="server" id="OpenHouseComments"><i class='fa fa-info-circle fa-lg' aria-hidden='true' title='<%# DataBinder.Eval(Container.DataItem,"OpenHouseComments")%>'></i></span></div>
         
      
      
 
    </div>
 </div> 

    </ItemTemplate>

            <EmptyDataTemplate>
               <h3>Nothing scheduled . . . Try another town?</h3>
            </EmptyDataTemplate>

</asp:ListView>

