using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;

using VfsCustomerService.Business;
using VfsCustomerService.Data;
using VfsCustomerService.Entities;
using VfsCustomerService.Utility;
using Vfs.WebCrawler.Utility;


public partial class CreateMessageFromExcel : System.Web.UI.Page
{
    protected string InputBodyMessagerID; 

    protected void Page_Load(object sender, EventArgs e)
    {
        contentTemplateDropDownList.Attributes.Add("onchange", "showContentTemplateDetail(this,'" + InputBodyMessager.ClientID + "');");
        
        if (!IsPostBack)
        {
            this.LoadTemplates();
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        importStep1Button.Value = Resources.UIResource.next;
        uploadFileRequiredFieldValidator.ErrorMessage = Resources.UIResource.NoFileSelected;
    }

    private void LoadTemplates()
    {
        this.contentTemplateDropDownList.DataSource = ContentTemplateService.GetContentTemplateList(ContentTemplateColumns.ModifiedDate, "DESC");
        this.contentTemplateDropDownList.DataTextField = "Description";
        this.contentTemplateDropDownList.DataValueField = "ContentTemplateID";
        this.contentTemplateDropDownList.DataBind();
        this.contentTemplateDropDownList.Items.Insert(0, new ListItem(Resources.UIResource.SelectOneItem, "0"));
    }

    protected void contentTemplateDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //if (contentTemplateDropDownList.SelectedIndex.ToString() != "0")
        //    InputBodyMessager.Text = ContentTemplateService.GetContentTemplate(Convert.ToInt32(contentTemplateDropDownList.Value.ToString())).BodyMessage;
        //else
        //    InputBodyMessager.Text = "";

    }

    protected void ImportStep1Button_Onclick(object sender, EventArgs e)
    {
        if (contentTemplateDropDownList.SelectedIndex.ToString() == "0")
        {
            successMessage.Text = Resources.UIResource.NoContentTemplateSelected;
            return;
        }

        if (SourceFile.FileName.Length == 0 || SourceFile.FileName == string.Empty)
        {
            successMessage.Text = Resources.UIResource.NoFileSelected;
            return;
        }

        string fileName = ImportService.UploadDocument(SourceFile.FileContent, ApplicationHelper.AttachementUploadFolderPath, SourceFile.FileName);
        string contentTemplateid = contentTemplateDropDownList.Value.ToString();
        int post = fileName.IndexOf('.');        

        Hashtable checkParam = null; 
        checkParam = VfsCustomerService.Business.ImportService.GetParameters(ContentTemplateService.GetContentTemplate(Convert.ToInt16(contentTemplateid)));
        if (checkParam != null)
        {
            if (checkParam.Count > 0)
            {
                Response.Redirect(String.Format("CreateMessageFromExcelStep2.aspx?{0}={1}&{2}={3}", AppConstants.QS_SESSION, fileName.Remove(post), AppConstants.QS_TEMPLATE_ID, contentTemplateid));
            }
            else
            {
                successMessage.Text = Resources.UIResource.errorInContentTemplate;
            }   
        }
        else
        {
            successMessage.Text = Resources.UIResource.errorInContentTemplate;
        }        
    }
}
