//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HopeEvents.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Events
    {
        public int id { get; set; }
        public int category_id { get; set; }
        public int user_id { get; set; }
        public Nullable<int> disability_id { get; set; }
        public string city { get; set; }
        public string metro { get; set; }
        public string street { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public System.DateTime date_of_publication { get; set; }
        public System.DateTime date_of_event { get; set; }
        public byte[] image_of_event { get; set; }
    
        public virtual Categories Categories { get; set; }
        public virtual Disabilities Disabilities { get; set; }
        public virtual Users Users { get; set; }
    }
}
