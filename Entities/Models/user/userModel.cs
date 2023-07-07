using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models.user
{
    public interface Iuser
    {
        long pk_fk_session_id { get; set; }
        int fk_user_id { get; set; }
        string name { get; set; }
        string mobile { get; set; }
        int appId { get; set; }
        string authentication { get; set; }
        userStatus userStatus { get; set; }
        int fk_store_id { get; set; }

        void map(Iuser from);
    }

    public class userModel : Iuser
    {
        public long pk_fk_session_id { get; set; }
        public int fk_user_id { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public int appId { get; set; }
        public string authentication { get; set; }
        public userStatus userStatus { get; set; } = userStatus.ananonymous;
        public int fk_store_id { get; set; }

        public void map(Iuser from)
        {
            this.appId = from.appId;
            this.authentication = from.authentication;
            this.fk_user_id = from.fk_user_id;
            this.mobile = from.mobile;
            this.name = from.name;
            this.pk_fk_session_id = from.pk_fk_session_id;
            this.userStatus = from.userStatus;
            this.fk_store_id = from.fk_store_id;
        }
    }

    public enum userStatus
    {
        ananonymous,
        loggedIn,
        unauthorized
    }
}
