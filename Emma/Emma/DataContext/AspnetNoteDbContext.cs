using Emma.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetNote.MVC6.DataContext
{
    //DbContext 상속 
    public class AspnetNoteDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //나는 Ms sql을 쓸 것이다. ("")-커넥션 스트링 https://www.connectionstrings.com/sql-server/
            //@는 이 문장을 정확히 전달하겠다는 뜻이다
            //서버 탐색기 explorer-데이터 연결-
            //서버이름 내 껄로 넣을 것
            
            //바꿔줘야 연결 됨
            optionsBuilder.UseSqlServer(@"Server = DESKTOP-NJEP1ES\SQLEXPRESS; Database = AspnetNoteDb; User Id = sa;Password = sa1234;");
           
        }
    }
}
