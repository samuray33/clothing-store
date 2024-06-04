//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Clothing_store
{
    using System;
    using System.Collections.Generic;
    
    public partial class main
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public main()
        {
            this.card = new HashSet<card>();
        }
    
        public int id { get; set; }
        public string nameItem { get; set; }
        public Nullable<int> price { get; set; }
        public string infoItem { get; set; }
        public Nullable<int> idCategor { get; set; }
        public Nullable<int> idColor { get; set; }
        public Nullable<int> idMaterial { get; set; }

        // если в бд нет фото то ставится заглушка
        public string photoItem { get; set; }

        public string Image { get; set; }
        public string CurrentPhoto
        {
            get 
            {
                if (String.IsNullOrEmpty(photoItem) || String.IsNullOrWhiteSpace(photoItem))
                {
                    return "/img/1.jpg";
                }
                else 
                {
                    return "/img/" + photoItem + ".jpg";
                }
            }
        }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<card> card { get; set; }
        public virtual categor categor { get; set; }
        public virtual color1 color1 { get; set; }
        public virtual material material { get; set; }
    }
}
