namespace MVCCourse_HomeWork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(vw_CustomerListMetaData))]
    public partial class vw_CustomerList
    {
    }
    
    public partial class vw_CustomerListMetaData
    {
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 客戶名稱 { get; set; }
        public Nullable<int> BankCount { get; set; }
        public Nullable<int> ContactCount { get; set; }
    }
}
