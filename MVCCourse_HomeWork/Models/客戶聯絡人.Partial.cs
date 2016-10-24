namespace MVCCourse_HomeWork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            CustomerCotactRepository repo = new CustomerCotactRepository();
            if(repo.All().Any(p => p.Email.Contains(this.Email) && p.客戶Id==this.客戶Id))
            {
                yield return new ValidationResult("此客戶下的 Email 已經被註冊過了!",
                    new string[] { "Email" });
            }
            yield break;
        }
    }

    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        [RegularExpression(@"\d{4}-\d{6}", ErrorMessage = "請輸入手機格式,範例:0911-123456")]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 電話 { get; set; }
        [Required]
        public bool Is刪除 { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
