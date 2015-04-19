// khi sua link hinh image can chu y sua : self.Message , self.Char, controller, click vao detail, 

function removeFileChart() {
        if (confirm("Bạn muốn xóa file hình này?")) {
            $('.chartImage').hide();
            $('.mb3-chart-thumb').removeAttr("src");
        }
    }

function uploadPreview(files) {
    file = files[0];

    if (file.size > 3000000) {
        showNotification('File hình quá lớn, xin vui lòng chọn hình khác?');
        return;
    }
    var ext = file.name.split('.').pop().toLowerCase();
    if ($.inArray(ext, ['png', 'jpg', 'jpeg']) == -1) {
        showNotification('File hình không đúng định dạng!. Chỉ hỗ trợ file hình png, jpg, jpeg');
        return;
    }
    // Add the uploaded image content to the form data collection
    if (files.length > 0) {
        var formData = new FormData();
        formData.append("UploadedImage", file);

        //upload via ajax
        $.ajax({
            url: '/Post/UploadFileChart',
            type: 'POST',
            data: formData,
            cache: false,
            contentType: false,
            processData: false
        }).done(function (data) {
            if (data === "error") {

                showNotification('Có lỗi khi upload ảnh, vui lòng thử lại');
                return
            }
            else {
                $('.chartImage').show();
                $('.mb3-chart-thumb').attr("src", data);
            }
        }).fail(function () {
            showNotification('Có lỗi khi upload ảnh, vui lòng thử lại');
        })
    }


}
function selectMe(e, data) {        // khi nguoi dung click vao link trong knockout js click event
    e.stopPropagation();
}

function setDefaultAfterPost() {
    $('.divBull, .divBear').parent().children('.switch3button-select').removeClass('switch3button-select'); // remove style of all radio button
    $('input[type=radio]').prop('checked', false); // remove checked of radio button
    //$("#status").css('height', '25'); // set size default of textarea
    //$('#status').parent().next('.status-control').hide(); // set hide button
    $('.chartImage').hide();
    $('.mb3-chart-thumb').removeAttr("src");
}
function getTimeAgo(varDate) {
    if (varDate) {
        return $.timeago(varDate);
    }
    else {
        return '';
    }
}
function Reply(data) {
    var self = this;
    data = data || {};
    self.ReplyId = data.ReplyId;
    self.ReplyMessage = data.ReplyMessage || "";
    self.ReplyByName = data.ReplyByName || "";
    self.ReplyByAvatar = data.ReplyByAvatar + '?width=46&height=46&mode=crop' || "";
    self.ReplyDate = getTimeAgo(data.ReplyDate);
    self.PostCommentsId = data.PostCommentsId;
}
function Post(data) {
    var self = this;
    data = data || {};
    self.PostId = data.PostId;
    self.Message = (data.ChartYN == 1 ? data.Message + '<br/><img src=' + data.Chart + '?width=215&height=120&mode=crop>' : data.Message) || "";
    self.PostedByName = data.PostedByName || "";
    self.PostedByAvatar = data.PostedByAvatar + '?width=50&height=50&mode=crop' || "";
    self.PostedDate = getTimeAgo(data.PostedDate);
    self.StockPrimary = data.StockPrimary;
    //self.notification = ko.observable(0);
    self.Stm = (data.Stm === 1 ? "<span class='divBear-cm'>Giảm</span>" : data.Stm === 2 ? "<span class='divBull-cm'>Tăng</span>" : "") || "";
    self.ChartYN = data.ChartYN || 0;
    self.SumLike = ko.observable(data.SumLike);
    self.DiableLike = ko.observable(true);
    self.Chart = data.Chart || '';
    self.SumReply = ko.observable(data.SumReply);
}
var commenthub = $.connection.CommentHub;
function viewModel() {
    var self = this;
    self.posts = ko.observableArray(); // danh muc post
    self.replys = ko.observableArray(); // danh mục reply
    self.postPins = ko.observableArray(); // danh muc cac bai post dc pin len dau
    self.newMessage = ko.observable('$' + $('#stockHidenPage').val() + ' '); // noi dung tin post $('#stockHidenPage').val()
    self.newReply = ko.observable(''); // noi dung tin reply
    self.error = ko.observable();
    self.newPosts = ko.observableArray(); // biến tạm để luu post moi, sau khi click thi moi bung ra
    self.postDetail = ko.observableArray();
    self.messageCount = ko.observable(0);
    self.replyCount = ko.observable(0);
    // SignalR related

    var checkpost = '';
    var checkreply = '';
    var postidCurrent = 0;
    var checkLoadFirst = 0;
    var filterhere = "";

    self.init = function () {
        self.error(null);
        commenthub.server.joinRoom($('#stockHidenPage').val());
        // lay danh muc pin bai viet
        $.ajax({
            cache: false,
            type: "GET",
            url: '/Post/GetPostsByStockPin',
            data: { stockCurrent: $('#stockHidenPage').val() },
            beforeSend: function (xhr) {
                //Add your image loader here
                //$('.ajaxLoadingImage').html('<img src="/images/ajax-loader_cungphim.gif" />');
            },
            success: function (data) {
                //$('.ajaxLoadingImage').html('');
                var mappedPosts = $.map(ko.utils.parseJson(data), function (item) { return new Post(item); });
                $(mappedPosts).each(function (index, element) {
                    self.postPins.push(element);
                });
            }
        });
        // lay danh muc thuong
        $.ajax({
            cache: false,
            type: "GET",            
            url: '/Post/GetMorePostsByStock',
            data: { stockCurrent: $('#stockHidenPage').val(), skipposition: 0, filter: "ALL" },
            beforeSend: function (xhr) {
                //Add your image loader here
                //$('.ajaxLoadingImage').html('<img src="/images/ajax-loader_cungphim.gif" />');
            },
            success: function (data) {
                //$('.ajaxLoadingImage').html('');
                var mappedPosts = $.map(ko.utils.parseJson(data), function (item) { return new Post(item); });
                $(mappedPosts).each(function (index, element) {
                    self.posts.push(element);
                });
                checkLoadFirst = 1;
            }
        });
    }
    /////////////////////////////////////////////////////
    self.addPost = function () { // them post
        self.error(null);
        $('#btAddPost').attr("disabled", true); // disble ngay khong de click them
        var nhanDinh = $("input:radio[name='BullBear']:checked").val();
        if (nhanDinh == null) {
            nhanDinh = 0;
        }
        var charImage = $('.mb3-chart-thumb').attr("src");
        if (charImage == null || charImage == '') {
            charImage = "";
        }

        commenthub.server.addPost({ "Message": self.newMessage() }, $('#stockHidenPage').val(), $('#HiddentCureentUserId').val(), $('#HiddentUserName').val(), $('#HiddentAvataEmage').val(), nhanDinh, charImage)
            .done(function () {
                showNotification('Bạn đã đăng bài thành công!');
            })
            .fail(function (err) {
                self.error(err);
            });
        checkpost = 'Y';
        self.newMessage('$' + $('#stockHidenPage').val() + ' ');//
        setDefaultAfterPost();
    }

    self.addReply = function () { // them tra loi
        $('#btAddReply').attr("disabled", true); // disble ngay khong de click them

        commenthub.server.addReply({ "Message": self.newReply(), "PostedBy": postidCurrent }, $('#stockHidenPage').val(), $('#HiddentCureentUserId').val(), $('#HiddentUserName').val(), $('#HiddentAvataEmage').val())
            .done(function () {
                showNotification('Bạn đã trả lời thành công!');
            })
            .fail(function (err) {
                self.error(err);
            });
        checkreply = 'Y';
        self.newReply('');
    }
    // nhan dc notification tu user khac
    commenthub.client.MessegeOfUserPost = function (number) {
        var num = $(".MessegeNofity").html();
        if (num != null) {
            $(".MessegeNofity").html(parseInt(num) + number);
        }
        else {
            $("#CreateMesseger").html("<span class='MessegeNofity'>1</span>");
        }
    }

    //////////////// load lại filter nè
    var filterhere = "";
    self.FilterAll = function (stringFilter) {
        if (checkLoadFirst == 1) {
            filterhere = stringFilter;
            self.posts([]);
            self.newPosts([]);
            $.ajax({
                cache: false,
                type: "GET",                

                url: '/Post/GetMorePostsByStock',
                data: { stockCurrent: $('#stockHidenPage').val(), skipposition: 0, filter: filterhere },
                beforeSend: function (xhr) {
                    //Add your image loader here
                    //$('.ajaxLoadingImage').html('<img src="/images/ajax-loader_cungphim.gif" />');
                },
                success: function (data) {
                    //$('.ajaxLoadingImage').html('');
                    var mappedPosts = $.map(ko.utils.parseJson(data), function (item) { return new Post(item); });
                    $(mappedPosts).each(function (index, element) {
                        self.posts.push(element);
                    });
                    checkLoadFirst = 1;
                }
            });
        }


    }

    /////////////////////////////////////////////////////////////
    // recieve the post from server
    commenthub.client.addPost = function (post) {
        if (checkpost == 'Y') {
            //filter here
            if (filterhere == "" || filterhere == "ALL") {
                self.posts.splice(0, 0, new Post(post));
            }
            if (filterhere == "CHA") {
                if (post.ChartYN) {
                    self.posts.splice(0, 0, new Post(post));
                }
            }
            if (filterhere == "STM") {
                if (post.Stm > 0) {
                    self.posts.splice(0, 0, new Post(post));
                }
            }

        }
        else {
            //filter here
            if (filterhere == "" || filterhere == "ALL") {
                self.newPosts.splice(0, 0, new Post(post));
                document.title = '(' + self.newPosts().length + ') ' + $('#titleHidenPage').val();
            }
            if (filterhere == "CHA") {
                if (post.ChartYN) {
                    self.newPosts.splice(0, 0, new Post(post));
                    document.title = '(' + self.newPosts().length + ') ' + $('#titleHidenPage').val();
                }
            }
            if (filterhere == "STM") {
                if (post.Stm > 0) {
                    self.newPosts.splice(0, 0, new Post(post));
                    document.title = '(' + self.newPosts().length + ') ' + $('#titleHidenPage').val();
                }
            }

        }
        checkpost = 'N';
    }

    commenthub.client.addReply = function (reply) {
        self.replys.unshift(new Reply(reply));
        //self.replys.splice(0, 0, new Reply(reply));
    }

    /////////////////////

    self.loadNewPosts = function () {
        self.posts(self.newPosts().concat(self.posts()));
        self.newPosts([]);
        document.title = $('#titleHidenPage').val();
    }

    self.afterAdd = function (elem) {
        if (checkLoadFirst == 1) {
            //$(elem).hide().slideDown('slow');
            $(elem).hide().slideDown('slow')
        }
    };

    self.AddLike = function (data, e) {
        // ajax update  like with 

        data.DiableLike(false);
        e.stopPropagation(); // stop popup
        commenthub.server.addNewLike(data.PostId)
            .fail(function (err) {
                self.error(err);
            });

    };
    commenthub.client.addNewLike = function (postid) {
        var Postfind = ko.utils.arrayFirst(self.posts(), function (item) {
            return item.PostId === postid;
        });
        if (Postfind != null) {
            Postfind.SumLike(Postfind.SumLike() + 1);
            return;
        }

        // post pin
        var PostPin = ko.utils.arrayFirst(self.postPins(), function (item) {
            return item.PostId === postid;
        });
        if (PostPin != null) {
            PostPin.SumLike(PostPin.SumLike() + 1);
            return;
        }
        // post nam tren local chua dc load
        var PostNewPost = ko.utils.arrayFirst(self.newPosts(), function (item) {
            return item.PostId === postid;
        });
        if (PostNewPost != null) {
            PostNewPost.SumLike(PostNewPost.SumLike() + 1);
            return;
        }



    }
    self.detailPost = function (data, e) { // chi tiet post bao gom tra loi

        self.newReply('');
        self.replys([]);
        $("#idPostedDateDetail").html(data.PostedDate);
        $("#idPostNameDetail").html(data.PostedByName).attr('href', '/user/' + data.PostedByName + '/tab/1');
        $("#idImgPostDetail").attr('src', data.PostedByAvatar);
        // relace hình bỏ. bỏ hình to vào
        $("#idPostMessenge").html(data.ChartYN == 1 ? data.Message.replace('<br/><img src=' + data.Chart + '?width=215&height=120&mode=crop>', '') + '<br/><br/><a target="_blank" href=' + data.Chart + '><img class="imageChartDetail" src=' + data.Chart + "?maxwidth=475></a>" : data.Message);//=200&s.grayscale=true|"
        $("#idStmDetail").html(data.Stm);
        postidCurrent = data.PostId;
        $("#IdLoadMoreConversation").attr('href', '/PostDetail?postid=' + postidCurrent);
        $("#facebookLikeAndShare").html("<script>$(document).ready(function() {try {FB.XFBML.parse();} catch (ex) { }});<\/script><div style='overflow: hidden;float:right' class='fb-like' data-href='/PostDetail?postid=" + postidCurrent + "' data-layout='button_count' data-action='like' data-show-faces='true' data-share='true'></div>");
        //data.notification(0);
        // load 10 reply gan nhat
        $.ajax({
            cache: false,
            type: "GET",
            url: '/Post/GetReplyByPostId',
            data: { replyid: data.PostId },
            beforeSend: function (xhr) {
                //Add your image loader here

            },
            success: function (data) {
                var mappedPosts = $.map(ko.utils.parseJson(data), function (item) { return new Reply(item); });
                $(mappedPosts).each(function (index, element) {
                    self.replys.push(element);
                });
            }

        });

        if (!$(e.target).hasClass('btnMore')) {
            $('#bg_dialog').show();
            dialog.dialog("open");
            $(".ui-widget-overlay, #bg_dialog").click(function (e) {
                if ($(e.target).parents('#bg_dialog').length == 0) {
                    dialog.dialog('close');
                }
            })
        }
    }
    /////////////////////////////////////////

    self.enablePhimHang = ko.computed(function () {        
        return 200 - self.messageCount() <= 200 && 200 - self.messageCount() > 6 && self.newMessage().indexOf('<', 0) == -1;
    });    
    
    
    self.count = ko.computed(function () {        
        var countNum = 200;        
        var arrayMessage = self.newMessage().split(' ');
        arrayMessage.forEach(function (item) {
            if (item.indexOf('http') != -1 ) { // tim thay http link
                countNum = countNum - 12;
            }
            else { // khong thay http link
                countNum = countNum - item.length - 1;
            }            
        });
        self.messageCount(countNum);       
        return countNum;
    });
    self.enablePhimHangReply = ko.computed(function () {
        return 140 - self.replyCount() <= 140 && 140 - self.replyCount() > 6 && self.newReply().indexOf('<', 0) == -1;
    });
    self.countReply = ko.computed(function () {
        var countNum = 140;
        var arrayMessage = self.newReply().split(' ');
        arrayMessage.forEach(function (item) {
            if (item.indexOf('http') != -1) { // tim thay http link
                countNum = countNum - 12;
            }
            else { // khong thay http link
                countNum = countNum - item.length - 1;
            }
        });
        self.replyCount(countNum);
        return countNum;

        
    });

    // notification of reply
    
    commenthub.client.newReplyNoti = function (postid) {
        // post chinh
            var replysfind = ko.utils.arrayFirst(self.posts(), function (item) {
                return item.PostId === postid;
            });
            if (replysfind != null) {
                replysfind.SumReply(replysfind.SumReply() + 1);
                return;
            }
        // post pin
            var replysfindPin = ko.utils.arrayFirst(self.postPins(), function (item) {
                return item.PostId === postid;
            });
            if (replysfindPin != null) {
                replysfindPin.SumReply(replysfindPin.SumReply() + 1);
                return;
            }
        // post nam tren local chua dc load
            var replysfindNewPost = ko.utils.arrayFirst(self.newPosts(), function (item) {
                return item.PostId === postid;
            });
            if (replysfindNewPost != null) {
                replysfindNewPost.SumReply(replysfindNewPost.SumReply() + 1);
                return;
            }


        //alert(self.notification());
    }
    // end
    var loadSlow = 'Y';
    $(window).scroll(function () { // scroll endpage load more
        if (document.documentElement.clientHeight + $(document).scrollTop() >= document.body.offsetHeight && checkLoadFirst == 1 && loadSlow == 'Y') {
            loadSlow = 'N';
            $('.ajaxLoadingImage').html('<img src="/images/ajax-loader_cungphim.gif" />');
            $.ajax({
                cache: false,
                type: "GET",
                url: '/Post/GetMorePostsByStock',
                data: { stockCurrent: $('#stockHidenPage').val(), skipposition: self.posts().length + self.newPosts().length, filter: filterhere },
                beforeSend: function (xhr) {
                    //Add your image loader here

                },
                success: function (data) {
                    $('.ajaxLoadingImage').html('');
                    var mappedPosts = $.map(ko.utils.parseJson(data), function (item) { return new Post(item); });
                    $(mappedPosts).each(function (index, element) {
                        self.posts.push(element);
                    });
                    loadSlow = 'Y';
                }
            })
        }
    });
    //@*self.loadMores = function () { // when end page
    //    $.get('@Url.Action("GetCommentBySymbol", "FollowStock")',
    //    {
    //        stockCurrent: '@ViewBag.StockCode',
    //        skipposition: self.posts().length
    //    }, function (data) {

    //        var mappedPosts = $.map(ko.utils.parseJson(data), function (item) { return new Post(item); });
    //        $(mappedPosts).each(function (index, element) {
    //            self.posts.push(element);
    //        });
    //    })
    //};*@
    }

ko.bindingHandlers.limitCharacters = {
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        element.value = element.value.substr(0, valueAccessor());
        allBindingsAccessor().value(element.value.substr(0, valueAccessor()));
    }
};
//var chromeframe = Request.UserAgent != null && Request.UserAgent.Contains("chromeframe");
//var transports = chromeframe ? "{ transport: 'longPolling' }" : "";
var vmPost = new viewModel();
ko.applyBindings(vmPost, document.getElementById("CommentForKoLoad"));
$.connection.hub.start({ transport: ['webSockets', 'serverSentEvents', 'longPolling'] })
                        .done(function () {
                            vmPost.init();
                            $('.ajaxLoadingInit').html('');

                        }).fail(function (fail) {
                            console.log('Không thể mở kết nối đến server' + fail);
                        });

$.connection.hub.disconnected(function () {
    setTimeout(function () {
        $.connection.hub.start().done(function () {
            commenthub.server.joinRoom($('#stockHidenPage').val());
        });
    }, 10000); // Restart connection after 10 seconds.
});
$.connection.hub.connectionSlow(function () {
    console.log('Kết nối đến server chậm do đường truyền')
});
window.onbeforeunload = function (e) {
    //$.connection.hub.stop
    //commenthub.server.stop = function () {
    //    $.connection.hub.stop();

    //    console.log('window.onbeforeunload');
    //};
};


//$.connection.hub.logging = true;

