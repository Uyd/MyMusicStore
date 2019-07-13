﻿using DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace aspWeb
{
    public partial class ProductList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //页面初次加载时，加载数据
            if (!IsPostBack)
            {
                _getData();
            }
        }
        private void _getData()
        {
            using(var context=new StuDBContext())
            {
                //查询出product数据
                var dept = context.DepartMents.Select(n=>new {SortCode=n.SortCode,Name=n.Name }).OrderBy(x => x.SortCode).ToList();
                GridView1.DataSource = dept;
                GridView1.DataBind();
            }
        }
        //翻页事件
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            _getData();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //查询出该记录的主键
            var id = Guid.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
            using (var context = new StuDBContext())
            {
                //删除这条记录
                var delProduct = context.DepartMents.Find(id);
                context.DepartMents.Remove(delProduct);
                context.SaveChanges();
            }
            _getData();
        }

        //切换到编辑
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            _getData();
        }

        //取消编辑
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            _getData();
        }

        //保存修改
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //查询出该记录的主键
            var id = Guid.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
            using (var context = new StuDBContext())
            {
                //查询出要修改这条记录
                var p = context.DepartMents.Find(id);
                //读出gridview中用户编辑的字段,给每个允许修改的实体属性赋值
                //获取用户编辑的这一行
                var row = GridView1.Rows[e.RowIndex];
                var name = (row.Cells[0].Controls[0] as TextBox).Text.Trim();
                var dscn = (row.Cells[1].Controls[0] as TextBox).Text.Trim();

                p.Name = name;
                p.SortCode = dscn;
                context.SaveChanges();
            }

            GridView1.EditIndex = -1;
            _getData();
        }
    }
}