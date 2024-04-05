using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities.User;

namespace DataLayer.Entities.Permision
{
    public class RolePermision
    {
        [Key]
        public long Id { get; set; }
        public long RoleId { get; set; }
        public long PermissionId { get; set; }


        #region Relation

        [ForeignKey("PermissionId")]
        public Permision Permision { get; set; }
        public Role Role { get; set; }

        #endregion
    }
}
