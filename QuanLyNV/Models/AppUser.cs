using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace QuanLyNV.Models {
    public class AppUser : IdentityUser {
        [MaxLength (100)]
        public string FullName { set; get; }

        [MaxLength (255)]
        public string Address { set; get; }

        [DataType (DataType.Date)]
        public DateTime? Birthday { set; get; }

        //khong đúng kiểu dữ liệu nếu dữ liệu đã có string và ta updatedatabase sẽ báo lỗi
        //Trường hợp khác nó sẽ drop cái khai báo string và thay thế thành boolean khi đó lưu vào database sẽ đúng
        public string Gender { set; get; }

    }

}
