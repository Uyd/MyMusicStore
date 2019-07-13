using MusicStoreEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace MusicStore.Migrations
{
    public class GenreSeed
    {
        public static readonly MusicStoreEntity.EntityDbContext _dbContext = new MusicStoreEntity.EntityDbContext();

        public static void Seed()
        {
            _dbContext.Database.ExecuteSqlCommand("delete albums");
            _dbContext.Database.ExecuteSqlCommand("delete artists");
            _dbContext.Database.ExecuteSqlCommand("delete genres");

            var genres = new List<Genre>()
            {
                new Genre() {Name="摇滚" ,Description="Rock"},
                new Genre() {Name="爵士", Description="Jazz"},
                new Genre() {Name="重金属", Description="Metal"},
                new Genre() {Name="蓝调" ,Description="Blue"},
                new Genre() {Name="拉丁" ,Description="Latin"},
                new Genre() {Name="嘻哈", Description="HiHop"},
            };

            foreach (var g in genres)
                _dbContext.Genres.Add(g);
            _dbContext.SaveChanges();

            //歌手
            var artists = new List<Artist>()
            {
                new Artist() {Name = "陈伟霆", Sex = true, Description = "1985年11月21日出生于中国香港，华语影视男演员、歌手、主持人"},
                new Artist(){Name = "周笔畅", Sex = false, Description = "1985年7月26日出生于湖南长沙，华语女歌手、词曲创作人、演员、Begins品牌主理人兼设计总监。"},
                new Artist() {Name="薛之谦" ,Sex = true, Description = "1983-07-17 (巨蟹座)，中国内地男歌手，籍贯上海市"},
                new Artist() {Name="展展与罗罗" ,Sex = true, Description = "华语组合，由施展、罗中凯组成。成立于2016年10月1日，擅长作曲作词编曲。歌曲代表作《沙漠骆驼》等。"},
                new Artist() {Name="龙梅子" ,Sex = false, Description = "1988年8月18日出生于甘肃兰州，中国内地女歌手、演员。2005年，凭借歌曲《光彩第二街》而正式出道。"},
                new Artist() {Name="许嵩" ,Sex = true, Description = "2006年大学期间开始以网名“Vae”在互联网发表原创音乐作品；"},
                new Artist() {Name="刘珂矣" ,Sex = false, Description = "禅意歌者，音乐唱作人，自推出《半壶纱》《一袖云》等优质唱作作品以来，广受好评"},
                new Artist() {Name="林俊杰" ,Sex = true, Description = "2003年，发行首张全创作专辑《乐行者》。虽然时逢SARS，但他和团队不轻言放弃，"},
            };
            foreach (var a in artists)
                _dbContext.Artists.Add(a);
            _dbContext.SaveChanges();

            //专辑
            var albums=new List<Album>()
            {
                new Album() {Title = "至少还有你爱我",Price=8.99M,Genre = genres.Single(g=>g.Name=="蓝调"),Artist=artists.Single(a=>a.Name=="龙梅子"),AlbumArtUrl = "/Content/images/placeholder.jpg"},
                new Album() {Title = "都说",Price=8.99M,Genre = genres.Single(g=>g.Name=="蓝调"),Artist=artists.Single(a=>a.Name=="龙梅子"),AlbumArtUrl = "/Content/images/placeholder.jpg"},
                new Album() {Title = "我留在了远方",Price=8.99M,Genre = genres.Single(g=>g.Name=="蓝调"),Artist=artists.Single(a=>a.Name=="龙梅子"),AlbumArtUrl = "/Content/images/placeholder.jpg"},
                new Album() {Title = "沙漠骆驼",Price=8.99M,Genre = genres.Single(g=>g.Name=="摇滚"),Artist=artists.Single(a=>a.Name=="展展与罗罗"),AlbumArtUrl = "/Content/images/placeholder.jpg"},
                new Album() {Title = "风雨无阻",Price=8.99M,Genre = genres.Single(g=>g.Name=="蓝调"),Artist=artists.Single(a=>a.Name=="陈伟霆"),AlbumArtUrl = "/Content/images/placeholder.jpg"},
                new Album() {Title = "鬼迷心窍",Price=8.99M,Genre = genres.Single(g=>g.Name=="蓝调"),Artist=artists.Single(a=>a.Name=="陈伟霆"),AlbumArtUrl = "/Content/images/placeholder.jpg"},
                new Album() {Title = "半壶纱",Price=8.99M,Genre = genres.Single(g=>g.Name=="蓝调"),Artist=artists.Single(a=>a.Name=="刘珂矣"),AlbumArtUrl = "/Content/images/placeholder.jpg"},
                new Album() {Title = "风筝误",Price=8.99M,Genre = genres.Single(g=>g.Name=="拉丁"),Artist=artists.Single(a=>a.Name=="刘珂矣"),AlbumArtUrl = "/Content/images/placeholder.jpg"},
                new Album() {Title = "暖山",Price=8.99M,Genre = genres.Single(g=>g.Name=="拉丁"),Artist=artists.Single(a=>a.Name=="刘珂矣"),AlbumArtUrl = "/Content/images/placeholder.jpg"},
                new Album() {Title = "醉赤壁",Price=8.99M,Genre = genres.Single(g=>g.Name=="重金属"),Artist=artists.Single(a=>a.Name=="林俊杰"),AlbumArtUrl = "/Content/images/placeholder.jpg"},
                new Album() {Title = "江南",Price=8.99M,Genre = genres.Single(g=>g.Name=="重金属"),Artist=artists.Single(a=>a.Name=="林俊杰"),AlbumArtUrl = "/Content/images/placeholder.jpg"},
                new Album() {Title = "一千年以后",Price=8.99M,Genre = genres.Single(g=>g.Name=="重金属"),Artist=artists.Single(a=>a.Name=="林俊杰"),AlbumArtUrl = "/Content/images/placeholder.jpg"},
                new Album() {Title = "谁动了我的琴弦",Price=8.99M,Genre = genres.Single(g=>g.Name=="爵士"),Artist=artists.Single(a=>a.Name=="周笔畅"),AlbumArtUrl = "/Content/images/placeholder.jpg"},
                new Album() {Title = "演员",Price=8.99M,Genre = genres.Single(g=>g.Name=="嘻哈"),Artist=artists.Single(a=>a.Name=="薛之谦"),AlbumArtUrl = "/Content/images/placeholder.jpg"},
                new Album() {Title = "千百度",Price=8.99M,Genre = genres.Single(g=>g.Name=="蓝调"),Artist=artists.Single(a=>a.Name=="许嵩"),AlbumArtUrl = "/Content/images/placeholder.jpg"},
                new Album() {Title = "明智之举",Price=8.99M,Genre = genres.Single(g=>g.Name=="蓝调"),Artist=artists.Single(a=>a.Name=="许嵩"),AlbumArtUrl = "/Content/images/placeholder.jpg"},
                new Album() {Title = "惊鸿一面",Price=8.99M,Genre = genres.Single(g=>g.Name=="蓝调"),Artist=artists.Single(a=>a.Name=="许嵩"),AlbumArtUrl = "/Content/images/placeholder.jpg"},
                new Album() {Title = "灰色头像",Price=8.99M,Genre = genres.Single(g=>g.Name=="蓝调"),Artist=artists.Single(a=>a.Name=="许嵩"),AlbumArtUrl = "/Content/images/placeholder.jpg"},
                new Album() {Title = "叹服",Price=8.99M,Genre = genres.Single(g=>g.Name=="蓝调"),Artist=artists.Single(a=>a.Name=="许嵩"),AlbumArtUrl = "/Content/images/placeholder.jpg"},
                new Album() {Title = "素颜",Price=8.99M,Genre = genres.Single(g=>g.Name=="蓝调"),Artist=artists.Single(a=>a.Name=="许嵩"),AlbumArtUrl = "/Content/images/placeholder.jpg"},
                new Album() {Title = "有何不可",Price=8.99M,Genre = genres.Single(g=>g.Name=="蓝调"),Artist=artists.Single(a=>a.Name=="许嵩"),AlbumArtUrl = "/Content/images/placeholder.jpg"},
                new Album() {Title = "我乐意",Price=8.99M,Genre = genres.Single(g=>g.Name=="爵士"),Artist=artists.Single(a=>a.Name=="许嵩"),AlbumArtUrl = "/Content/images/placeholder.jpg"},
                new Album() {Title = "想你啦",Price=8.99M,Genre = genres.Single(g=>g.Name=="蓝调"),Artist=artists.Single(a=>a.Name=="龙梅子"),AlbumArtUrl = "/Content/images/placeholder.jpg"},
                new Album() {Title = "唱一首情歌",Price=8.99M,Genre = genres.Single(g=>g.Name=="蓝调"),Artist=artists.Single(a=>a.Name=="龙梅子"),AlbumArtUrl = "/Content/images/placeholder.jpg"},
                new Album() {Title = "谁都不要说分手",Price=8.99M,Genre = genres.Single(g=>g.Name=="蓝调"),Artist=artists.Single(a=>a.Name=="龙梅子"),AlbumArtUrl = "/Content/images/placeholder.jpg"},
                new Album() {Title = "只要平凡",Price=8.99M,Genre = genres.Single(g=>g.Name=="摇滚"),Artist=artists.Single(a=>a.Name=="陈伟霆"),AlbumArtUrl = "/Content/images/placeholder.jpg"},
                new Album() {Title = "My Sunshine",Price=8.99M,Genre = genres.Single(g=>g.Name=="摇滚"),Artist=artists.Single(a=>a.Name=="陈伟霆"),AlbumArtUrl = "/Content/images/placeholder.jpg"},
            };

            foreach (var al in albums)
                _dbContext.Albums.Add(al);
            _dbContext.SaveChanges();
        }

        public static void Extend()
        {
            var albums = _dbContext.Albums.ToList();
            foreach (var album in albums)
            {
                var item = _dbContext.Albums.Find(album.ID);
                item.GenreId = item.Genre.ID.ToString();
                item.ArtistId = item.Artist.ID.ToString();
                _dbContext.SaveChanges();
                Thread.Sleep(3);
            }
        }
    }
}