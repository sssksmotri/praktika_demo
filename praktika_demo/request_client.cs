//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace praktika_demo
{
    using System;
    using System.Collections.Generic;
    
    public partial class request_client
    {
        public int requestID { get; set; }
        public int clientID { get; set; }
        public Nullable<int> masterID { get; set; }
    
        public virtual request request { get; set; }
        public virtual user user { get; set; }
        public virtual user user1 { get; set; }
    }
}
