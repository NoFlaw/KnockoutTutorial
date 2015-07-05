
ko.extenders.required = function (target, overrideMessage) {
    //add some sub-observables to our observable
    target.hasError = ko.observable();
    target.validationMessage = ko.observable();

    //define a function to do validation
    function validate(newValue) {
        target.hasError(newValue ? false : true);
        target.validationMessage(newValue ? "" : overrideMessage || "This field is required");
    }

    //initial validation
    validate(target());

    //validate whenever the value changes
    target.subscribe(validate);

    //return the original observable
    return target;
};

function BlogModel(data) {
    var self = this;




    self.BlogId = ko.observable(data.BlogId);
    self.Author = ko.observable(data.Author).extend({required: ""});
    self.Title = ko.observable(data.Title).extend({ required: "" });
    self.Description = ko.observable(data.Description).extend({ required: "" });
    self.DateCreated = ko.observable(moment(data.DateCreated).format("L"));
    self.BlogFullName = ko.computed(function () {
        return self.Author() + ' ' + self.Title();
    });
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
        self.Blogs.push({
            BlogId: 0,
            Author: "",
            Title: "",
            Description: "",
            DateCreated: new Date().toLocaleString(),
            BlogFullName: ""
        });
    };

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
                self.Blogs.remove(blog);
                window.location.href = '/BlogsKnockout/Index/';
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
                self.Blogs.removeAll();
                window.location.href = '/BlogsKnockout/Index/';
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
                    alert(data.Message);
                    window.location.href = '/BlogsKnockout/Index/';
                }
            });
    };
}
