// khi sua link hinh image can chu y sua : self.Message , self.Char, controller, click vao detail, 
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
    //self.StockPrimary = data.StockPrimary;
    //self.notification = ko.observable(0);
    self.Stm = (data.Stm === 1 ? "<span class='divBear-cm'>Giảm</span>" : data.Stm === 2 ? "<span class='divBull-cm'>Tăng</span>" : "") || "";
    self.ChartYN = data.ChartYN || 0;
    self.XemYN = data.XemYN; // == 1 ? "New" : "" || "";
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
    self.newReply = ko.observable(''); // noi dung tin reply
    self.error = ko.observable();
    self.newPosts = ko.observableArray(); // biến tạm để luu post moi, sau khi click thi moi bung ra
    self.postDetail = ko.observableArray();
    self.replyCount = ko.observable(0);
    // SignalR related

    var checkpost = '';
    var checkreply = '';
    var postidCurrent = 0;
    var checkLoadFirst = 0;
    var filterhere = "";

    self.init = function () {
        self.error(null);
        self.posts([]);
        // lay danh muc messenges
        $.ajax({
            cache: false,
            type: "GET",
            url: '/MyProfile/GetMessagesByUserId',
            data: { userid: $('#HiddentCureentUserId').val(), skipposition: 0, filter: "ALL" },
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

        // xóa các messenges đã đọc
        $.ajax({
            cache: false,
            type: "POST",
            url: '/MyProfile/ChangeStatusMessege',
            data: { userid: $('#HiddentCureentUserId').val() },
            beforeSend: function (xhr) {
            },
            success: function () {
            }
        });
    }
    /////////////////////////////////////////////////////

    self.addReply = function () { // them tra loi
        commenthub.server.addReply({ "Message": self.newReply(), "PostedBy": postidCurrent })
               .done(function () {
                   showNotification('Bạn đã trả lời thành công!');
               })
            .fail(function (err) {
                self.error(err);
            });
        checkreply = 'Y';
        self.newReply('');
    }

    // nhan reply từ user
    commenthub.client.addReply = function (reply) {
        self.replys.unshift(new Reply(reply));
        //self.replys.splice(0, 0, new Reply(reply));
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
    /////////////////////////////////////////////////////////////
    // recieve the post from server


    self.detailPost = function (data, e) { // chi tiet post bao gom tra loi

        self.newReply('');
        self.replys([]);
        $("#idPostedDateDetail").html(data.PostedDate);
        $("#idPostNameDetail").html('<a style="cursor:pointer" href="/' + data.PostedByName + '">' + data.PostedByName + '</a>');
        $("#idImgPostDetail").attr('src', data.PostedByAvatar);
        $("#idPostMessenge").html(data.ChartYN == 1 ? data.Message.replace('<br/><img src=' + data.Chart + '?width=215&height=120&mode=crop>', '') + '<br/><br/><a target="_blank" href=' + data.Chart + '><img class="imageChartDetail" src=' + data.Chart + "?maxwidth=475></a>" : data.Message);//=200&s.grayscale=true|"
        $("#idStmDetail").html(data.Stm);
        postidCurrent = data.PostId;
        $("#IdLoadMoreConversation").attr('href', '/PostDetail?postid=' + postidCurrent);
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
            $('#close_dialog').show();
            dialog.dialog("open");
            $(".ui-widget-overlay, #bg_dialog, #close_dialog").click(function (e) {
                if ($(e.target).parents('#bg_dialog').length == 0) {
                    dialog.dialog('close');
                }
            })
        }
    }
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

    }

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
        //
        var replysfind = ko.utils.arrayFirst(self.posts(), function (item) {
            return item.PostId === postid;
        });
        if (replysfind != null) {
            replysfind.SumReply(replysfind.SumReply() + 1);
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
                url: '/MyProfile/GetMessagesByUserId',
                data: { userid: $('#HiddentCureentUserId').val(), skipposition: self.posts().length, filter: "ALL" },
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
        $.connection.hub.start();
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