namespace HelpdeskDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employee: EmployeeEntity
    {

        // remove ID since we have employee entitiy
        //   public int Id { get; set; }

        [StringLength(4)]
        public string Title { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(25)]
        public string PhoneNo { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public int DepartmentId { get; set; }

        public bool? IsTech { get; set; }

        public byte[] StaffPicture { get; set; }


        // remove timer since we have employee entitiy
        //[Column(TypeName = "timestamp")]
        //[MaxLength(8)]
        //[Timestamp]
        //public byte[] Timer { get; set; }

        public virtual Department Department { get; set; }
    }
}
