<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="GIBS.Modules.FlexMLS_OpenHouse.Settings" %>

<%@ Register TagName="label" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>

<h2 id="dnnSitePanel-BasicSettings" class="dnnFormSectionHead">
    <a href="" class="dnnSectionExpanded"><%=LocalizeString("BasicSettings")%></a></h2>
<fieldset>
    <div class="dnnFormItem">
    <dnn:label id="lblFlexMLSModule" runat="server" suffix=":" controlname="ddlFlexMLSModule" />
	<asp:dropdownlist id="ddlFlexMLSModule" Runat="server" datavaluefield="TabID" datatextfield="TabName"></asp:dropdownlist>
    </div>

    <div class="dnnFormItem">
        <dnn:label ID="lblTown" runat="server" suffix=":" ControlName="txtTown">
        </dnn:label>
        <asp:TextBox ID="txtTown" runat="server" />
    </div>
	
    <div class="dnnFormItem">
    <dnn:label id="lblMLSImagesUrl" runat="server" controlname="txtMLSImagesUrl" suffix=":" />
            <asp:textbox id="txtMLSImagesUrl" runat="server" Text="https://gibs.com/Images/" />		
    </div>

	
	
</fieldset>