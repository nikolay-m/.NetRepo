$(document).ready(function () {

    var found = false;

    $("#tab-container ul li").click(function () {
        $("#tab-container ul li").each(function () {
            $(this).removeClass("selected");
        });

        var active = "#".concat($(this).attr("id"));
        var activeContainer = "#c".concat($(this).attr("id"));
        
        (function ($) {
            jQuery.fn.hideDiv = function () {
                $("#main-container div").each(function () {
                    $("#main-container div h3").each(function () {
                        $(this).next("div").fadeOut();
                    });
                    $(this).hide();
                });
            };
        })(jQuery);

        (function ($) {
            jQuery.fn.showDiv = function (container) {
                $(container).show("slide", { direction: "down" }, 100, function () {
                    $(container.concat(" div")).show("slide", { direction: "left" }, 1000);
                });
                
            }
        })(jQuery);

        $(active).addClass("selected");
        $().hideDiv();
        $().showDiv(activeContainer);
    });
        if (!found) {
            $("#tab-container li:first").addClass("selected");
            $("#main-container div:not(first)").hide();
            $("#main-container div:first").show("slide", { direction: "down" }, 200,function () {
                $(this).children(".text").show("slide", {direction:"left"}, 1000);
            });
        }
});

