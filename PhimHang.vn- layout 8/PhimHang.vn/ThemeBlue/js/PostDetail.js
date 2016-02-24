//cap nhat controller them sumreply, them self.SumReply, copy bieu tuong , sua lai ham nhan reply
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

function Reply(data) {
    var self = this;
    data = data || {};
    self.ReplyId = data.ReplyId;
    self.ReplyMessage = data.ReplyMessage || "";
    self.ReplyByName = data.ReplyByName || "";
    self.ReplyByAvatar = data.ReplyByAvatar + '?width=46&height=46&mode=crop' || "";
    self.ReplyDate = getTimeAgo(data.ReplyDate);
    self.PostCommentsId = data.PostCommentsId;
    self.ReplyBrkVip = (data.BrkVip == 1 ? '<i title="Đã xác thực - dân phím chuyên nghiệp" class="fa  fa-check-circle"></i>' : "") || "";
}
function Post(data) {
    var self = this;
    data = data || {};
    self.PostId = data.PostId;
    self.Message = data.Message || "";
    self.PostedByName = data.PostedByName || "";
    self.PostedByAvatar = data.PostedByAvatar + '?width=50&height=50&mode=crop' || "";
    self.PostedDate = getTimeAgo(data.PostedDate);
    self.StockPrimary = data.StockPrimary;    
    //self.notification = ko.observable(0);
    self.Stm = (data.Stm === 1 ? "<span class='divBear-cm'>Giảm</span>" : data.Stm === 2 ? "<span class='divBull-cm'>Tăng</span>" : "") || "";
    self.ChartYN = data.ChartYN || 0;
}
var commenthub = $.connection.CommentHub;
function viewModel() {
    var self = this;
    self.posts = ko.observableArray(); // danh muc post
    self.replys = ko.observableArray(); // danh mục reply
    self.postPins = ko.observableArray(); // danh muc cac bai post dc pin len dau
    //self.newMessage = ko.observable('$@ViewBag.StockCode '); // noi dung tin post
    self.newReply = ko.observable(''); // noi dung tin reply
    self.error = ko.observable();
    self.newPosts = ko.observableArray(); // biến tạm để luu post moi, sau khi click thi moi bung ra
    self.postDetail = ko.observableArray();
    self.replyCount = ko.observable(0);
    self.SumLike = ko.observable($('#HiddentsumLike').val());
    self.DiableLike = ko.observable(true);
    // SignalR related

    var checkpost = '';
    var checkreply = '';
    var postidCurrent = 0;
    var checkLoadFirst = 0;
    var filterhere = "";

    self.init = function () {
        self.error(null);
        $.ajax({
            cache: false,
            type: "GET",
            url: '/PostDetail/GetReplyByPostId',
            data: { replyid: $('#hiddenPostId').val() },
            beforeSend: function (xhr) {
                //Add your image loader here
            },
            success: function (data) {
                var mappedPosts = $.map(ko.utils.parseJson(data), function (item) { return new Reply(item); });
                $(mappedPosts).each(function (index, element) {
                    self.replys.push(element);
                });
                // load post
                $("#idPostedDateDetail").html(getTimeAgo($('#HiddentPostedDate').val())).attr('data-title', $('#HiddentPostedDate').val());
                $("#idPostNameDetail").html('<a style="cursor:pointer" href="/' + $('#HiddentPostedByName').val() + '">' + $('#HiddentPostedByName').val() + '</a>' );
                $("#idImgPostDetail").attr('src', $('#HiddentPostedByAvatar').val() + '?width=50&height=50&mode=crop');
                //$("#idStmDetail").html(data.Stm);
            }
        });
    }        
    self.addReply = function () { // them tra loi
        $('#btAddReply').attr("disabled", true); // disble ngay khong de click them
        commenthub.server.addReply({ "Message": self.newReply(), "PostedBy": $('#hiddenPostId').val() })
            .done(function (status) {
                if (status == "L") {
                    showNotification("<b style='color:red'>User tạm thời đang bị khóa</b>");
                }
                else {
                    showNotification('Trả lời bài viết thành công!');
                }
                // tổng reply +  thêm 1
                var num = $(".list-item-control i").html();
                if (num != null) {
                    $(".list-item-control i").html(parseInt(num) + 1);
                }
                else {
                    $(".list-item-control").html("<a class='button' href='#'><i class='fa fa-comments'>1</i></a>");
                }
                ///
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
    self.AddLike = function () {
        // ajax update  like with 
        self.DiableLike(false);
        commenthub.server.addNewLike($('#hiddenPostId').val())
            .done(function () {
                // like thành cong. tang like
                self.SumLike(parseInt($('#HiddentsumLike').val()) + 1);
            })
            .fail(function (err) {
                self.error(err);
            });

    };

    self.afterAdd = function (elem) {
        if (checkLoadFirst == 1) {
            //$(elem).hide().slideDown('slow');
            $(elem).hide().slideDown('slow')
        }
    };
    ////////////////////////////
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
        
}

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
