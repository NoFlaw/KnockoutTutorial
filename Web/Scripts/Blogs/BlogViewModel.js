function BlogModel(data) {
    var self = this;

    self.BlogId = ko.observable(data.BlogId);
    self.Author = ko.observable(data.Author);
    self.Title = ko.observable(data.Title);
    self.Description = ko.observable(data.Description);
    self.DateCreated = ko.observable(moment(data.DateCreated).format("L"));

    self.inputChanged = function (blog) {
        var blogToPost = ko.toJSON(blog);
        $.ajax({
            type: 'POST',
            data: blogToPost,
            url: '/BlogsKnockout/SaveBlog',
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            success: function (d) {
                alert(d.message);
            },
            error: function (xhr) {
                alert('StatusCode: ' + xhr.status + ' ' + 'Error:' + xhr.responseText);
                console.debug(xhr);
            }
        });
    }

    self.BlogFullName = ko.computed(function () {
        return self.Author() + ' ' + self.Title();
    });

    self.SaveBlog = function (blog) {
        var blogToPost = ko.toJSON(blog);
        $.ajax({
            type: 'POST',
            data: blogToPost,
            url: '/BlogsKnockout/SaveBlog',
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            success: function (d) {
                alert(d.message);
            },
            error: function (xhr) {
                alert('StatusCode: ' + xhr.status + ' ' + 'Error:' + xhr.responseText);
                console.debug(xhr);
            }
        });
    }
}

function BlogViewModel() {
    var self = this;
    self.Blogs = ko.observableArray([]);

    self.GetBlogs = $.getJSON("/BlogsKnockout/GetAllBlogs", function (data) {
        self.Blogs($.map(data, function (item) {
            return new BlogModel(item);
        }));
    });

    self.AddBlog = function () {
        
        self.Blogs.push( new BlogModel({
            BlogId: 0,
            Author: "",
            Title: "",
            Description: "",
            DateCreated: new Date().toLocaleString(),
            BlogFullName: ""
        }));
    };

    self.canSave = function() {
        return self.Blogs().length > 0;
    }

    self.RemoveBlog = function (blog) {

        if (blog.BlogId <= 0) {
            self.Blogs.remove(blog);
            return;
        }

        $.ajax({
            type: 'POST',
            url: '/BlogsKnockout/RemoveBlog/' + ko.toJSON(blog.BlogId),
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            async: true,
            error: function (xhr) {
                alert('StatusCode: ' + xhr.status + ' ' + 'Error:' + xhr.responseText);
                console.debug(xhr);
            },
            success: function (data) {
                alert(data.message);

                if (!data.success) {
                    window.location.href = '/BlogsKnockout/Index/';
                }
                self.Blogs.remove(blog);
            }
        });
    };

    self.RemoveAllBlogs = function () {

        var blogsToBeRemoved = ko.toJSON(self.Blogs);

        $.ajax({
            type: 'POST',
            url: '/BlogsKnockout/RemoveAllBlogs/',
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            async: true,
            data: blogsToBeRemoved,
            error: function (xhr) {
                alert('StatusCode: ' + xhr.status + ' ' + 'Error:' + xhr.responseText);
                console.debug(xhr);
            },
            success: function (data) {
                alert(data.message);

                if (!data.success) {
                    window.location.href = '/BlogsKnockout/Index/';
                }

                self.Blogs.removeAll();
            }
        });
    };

    self.Save = function () {

        var blogsToBeUpdated = ko.toJSON(self.Blogs);

        $.ajax({
            type: "POST",
            url: '/BlogsKnockout/AddOrUpdateBlogs',
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            async: true,
            data: blogsToBeUpdated,
            error: function (xhr) {
                alert('StatusCode: ' + xhr.status + ' ' + 'Error:' + xhr.responseText);
                console.debug(xhr);
            },
            success: function (data) {
                alert(data.message);
                window.location.href = '/BlogsKnockout/Index/';
            }
        });
    };
}
