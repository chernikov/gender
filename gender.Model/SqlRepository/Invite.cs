using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace gender.Model
{
	
 public partial class SqlRepository
    {
        public IQueryable<Invite> Invites
        {
            get
            {
                return Db.Invites;
            }
        }

        public bool CreateInvite(Invite instance)
        {
            if (instance.ID == 0)
            {
                Db.Invites.InsertOnSubmit(instance);
                Db.Invites.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool UpdateInvite(Invite instance)
        {
            var cache = Db.Invites.FirstOrDefault(p => p.ID == instance.ID);
            if (cache != null)
            {
				cache.UserID = instance.UserID;
				cache.InvitedID = instance.InvitedID;
				cache.Email = instance.Email;
				cache.Link = instance.Link;
				cache.AddedDate = instance.AddedDate;
				cache.UsedDate = instance.UsedDate;
                Db.Invites.Context.SubmitChanges();
                return true;
            }

            return false;
        }

        public bool RemoveInvite(int idInvite)
        {
            Invite instance = Db.Invites.FirstOrDefault(p => p.ID == idInvite);
            if (instance != null)
            {
                Db.Invites.DeleteOnSubmit(instance);
                Db.Invites.Context.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}