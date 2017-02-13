using System;

namespace TuvaletBekcisi
{
    public class Request
    {
        public Guid RequestId { get; set; }
        public DateTime RequestedDate { get; set; }
        public Guid UserId { get; set; }
        public RequestStatus State { get; set; }
        public virtual User User { get; set; }
    }
    public enum RequestStatus : short
    {
        Beklemede = 0,
        Bilgilendirildi = 1,
        İşleniyor = 2,
        Tamamlandı = 3
    }
}