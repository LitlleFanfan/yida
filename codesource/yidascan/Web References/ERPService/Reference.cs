﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.42000 版自动生成。
// 
#pragma warning disable 1591

namespace yidascan.ERPService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="WebServiceSoap", Namespace="http://tempuri.org/")]
    public partial class WebService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback getDataForLocationOperationCompleted;
        
        private System.Threading.SendOrPostCallback getDataForWeightOperationCompleted;
        
        private System.Threading.SendOrPostCallback getDataForFinishOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public WebService() {
            this.Url = global::yidascan.Properties.Settings.Default.yidascan_ERPService_WebService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event getDataForLocationCompletedEventHandler getDataForLocationCompleted;
        
        /// <remarks/>
        public event getDataForWeightCompletedEventHandler getDataForWeightCompleted;
        
        /// <remarks/>
        public event getDataForFinishCompletedEventHandler getDataForFinishCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/getDataForLocation", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string getDataForLocation(string str) {
            object[] results = this.Invoke("getDataForLocation", new object[] {
                        str});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void getDataForLocationAsync(string str) {
            this.getDataForLocationAsync(str, null);
        }
        
        /// <remarks/>
        public void getDataForLocationAsync(string str, object userState) {
            if ((this.getDataForLocationOperationCompleted == null)) {
                this.getDataForLocationOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetDataForLocationOperationCompleted);
            }
            this.InvokeAsync("getDataForLocation", new object[] {
                        str}, this.getDataForLocationOperationCompleted, userState);
        }
        
        private void OngetDataForLocationOperationCompleted(object arg) {
            if ((this.getDataForLocationCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getDataForLocationCompleted(this, new getDataForLocationCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/getDataForWeight", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string getDataForWeight() {
            object[] results = this.Invoke("getDataForWeight", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void getDataForWeightAsync() {
            this.getDataForWeightAsync(null);
        }
        
        /// <remarks/>
        public void getDataForWeightAsync(object userState) {
            if ((this.getDataForWeightOperationCompleted == null)) {
                this.getDataForWeightOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetDataForWeightOperationCompleted);
            }
            this.InvokeAsync("getDataForWeight", new object[0], this.getDataForWeightOperationCompleted, userState);
        }
        
        private void OngetDataForWeightOperationCompleted(object arg) {
            if ((this.getDataForWeightCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getDataForWeightCompleted(this, new getDataForWeightCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/getDataForFinish", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string getDataForFinish(string Board_No, string AllBarCode) {
            object[] results = this.Invoke("getDataForFinish", new object[] {
                        Board_No,
                        AllBarCode});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void getDataForFinishAsync(string Board_No, string AllBarCode) {
            this.getDataForFinishAsync(Board_No, AllBarCode, null);
        }
        
        /// <remarks/>
        public void getDataForFinishAsync(string Board_No, string AllBarCode, object userState) {
            if ((this.getDataForFinishOperationCompleted == null)) {
                this.getDataForFinishOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetDataForFinishOperationCompleted);
            }
            this.InvokeAsync("getDataForFinish", new object[] {
                        Board_No,
                        AllBarCode}, this.getDataForFinishOperationCompleted, userState);
        }
        
        private void OngetDataForFinishOperationCompleted(object arg) {
            if ((this.getDataForFinishCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getDataForFinishCompleted(this, new getDataForFinishCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void getDataForLocationCompletedEventHandler(object sender, getDataForLocationCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getDataForLocationCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getDataForLocationCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void getDataForWeightCompletedEventHandler(object sender, getDataForWeightCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getDataForWeightCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getDataForWeightCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void getDataForFinishCompletedEventHandler(object sender, getDataForFinishCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getDataForFinishCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getDataForFinishCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591