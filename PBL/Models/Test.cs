﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL.Models
{
    public class Test : StudyElement
    {
        [DisplayName("ID")]
        public override int Id { get; set; }

        [DisplayName("Ten")]
        [Required(ErrorMessage = "Nhap ten.")]
        [StringLength(50, ErrorMessage = "Ten phai co do dai toi da 50 ki tu.")]
        public override string Name { get; set; }

        [DisplayName("Ngay dang")]
        public override DateTime PublishDay { get; set; }

        public int Id_Teacher { get; set; }
        [ForeignKey("Id_Teacher")]
        public virtual TeacherModel Teacher { get; set; }

        [DisplayName("Gioi thieu")]
        [Required(ErrorMessage = "Nhap noi dung gioi thieu.")]
        public string Description {  get; set; }

        [DisplayName("So luong tham gia")]
        public int Count {  get; set; }
    }
}
