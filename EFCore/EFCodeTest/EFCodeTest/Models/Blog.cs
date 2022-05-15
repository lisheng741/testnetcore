﻿namespace EFCodeTest.Models;

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }

    public List<Post>? Posts { get; set; } // 导航属性
}

public class Post
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public Blog? Blog { get; set; }
}