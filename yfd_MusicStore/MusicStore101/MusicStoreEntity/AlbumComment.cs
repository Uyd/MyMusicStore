using MusicStoreEntity.UserAndRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStoreEntity
{
    /// <summary>
    /// 用户对专辑的点评
    /// </summary>
    public class AlbumComment
    {
        public Guid ID { get; set; }
        public virtual string Title { get; set; }//标题
        public virtual Person Person { get; set; }
        public string Conmment { get; set; }//回复的内容
        public virtual AlbumComment ParentReply { get; set; }   //上级回复
        public DateTime CreateDateTime { get; set; }  //回复时间

        public AlbumComment()
        {
            ID = Guid.NewGuid();
            CreateDateTime=DateTime.Now;
        }
    }
}
