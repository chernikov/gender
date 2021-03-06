﻿// Generated by CoffeeScript 1.4.0

(function ($) {
    return $.fn.ajaxChosen = function (settings, callback, chosenOptions) {
        var chosenXhr, defaultOptions, options, select;
        if (settings == null) {
            settings = {};
        }
        if (chosenOptions == null) {
            chosenOptions = {};
        }
        defaultOptions = {
            minTermLength: 3,
            afterTypeDelay: 500,
            jsonTermKey: "term",
            keepTypingMsg: "Keep typing...",
            lookingForMsg: "Looking for"
        };
        select = this;
        chosenXhr = null;
        options = $.extend({}, defaultOptions, $(select).data(), settings);
        nItems = 0;
        noItems = false;
        chosenInst = this.chosen(chosenOptions ? chosenOptions : {});
        return this.each(function ()
        {
            return $(this).next('.chzn-container').find(".search-field > input, .chzn-search > input").bind('keyup', function (e) {
                var code = (e.keyCode ? e.keyCode : e.which);
                var field, msg, success, untrimmed_val, val;
                untrimmed_val = $(this).val();
                val = $.trim($(this).val());
                msg = val.length < options.minTermLength ? options.keepTypingMsg : options.lookingForMsg + (" '" + val + "'");
                select.next('.chzn-container').find('.no-results').text(msg);
                searchField = $(this);
               
                if (val === $(this).data('prevVal') && code != 13) {
                    return false;
                }
                if ($(this).data('prevVal') != null && val.length > $(this).data('prevVal').length && noItems && code != 13)
                {
                    return false;
                }
                $(this).data('prevVal', val);
                if (this.timer)
                {
                    clearTimeout(this.timer);
                }
                if (val.length < options.minTermLength)
                {
                    return false;
                }
                field = $(this);
                if (options.data == null)
                {
                    options.data = {};
                }
                options.data[options.jsonTermKey] = val;
                if (options.dataCallback != null)
                {
                    options.data = options.dataCallback(options.data);
                }
                success = options.success;
                options.success = function (data)
                {
                    var items, nbItems, selected_values;
                    if (data == null) {
                        return;
                    }
                    selected_values = [];
                    select.find('option').each(function () {
                        if (!$(this).is(":selected")) {
                            return $(this).remove();
                        } else {
                            return selected_values.push($(this).val() + "-" + $(this).text());
                        }
                    });
                    select.find('optgroup:empty').each(function () {
                        return $(this).remove();
                    });
                    items = callback != null ? callback(data, field) : data;
                   
                    nbItems = 0;
                    $.each(items, function (i, element) {
                        var group, text, value;
                        nbItems++;
                        if (element.group) {
                            group = select.find("optgroup[label='" + element.text + "']");
                            if (!group.size()) {
                                group = $("<optgroup />");
                            }
                            group.attr('label', element.text).appendTo(select);
                            return $.each(element.items, function (i, element) {
                                var text, value;
                                if (typeof element === "string") {
                                    value = i;
                                    text = element;
                                } else {
                                    value = element.value;
                                    text = element.text;
                                }
                                if ($.inArray(value + "-" + text, selected_values) === -1) {
                                    return $("<option />").attr('value', value).html(text).appendTo(group);
                                }
                            });
                        } else {
                            if (typeof element === "string") {
                                value = i;
                                text = element;
                            } else {
                                value = element.value;
                                text = element.text;
                            }
                            if ($.inArray(value + "-" + text, selected_values) === -1) {
                                return $("<option />").attr('value', value).html(text).appendTo(select);
                            }
                        }
                    });
                    if (nbItems) {
                        noItems = false;
                        select.trigger("liszt:updated");
                    } else {
                        noItems = true;
                        if (code == 13)
                        {
                            if (options.addCallback != null)
                            {
                                data = options.addCallback(val);
                                if (data != null)
                                {
                                    $("<option />").attr('value', data.value).attr("selected", true).html(data.text).appendTo(select);
                                    select.trigger("liszt:updated");
                                    searchField.val("");
                                    return false;
                                }
                            }
                        }
                    }
                    if (settings.success != null)
                    {
                        settings.success(data);
                    }
                    return field.val(untrimmed_val);
                };
                return this.timer = setTimeout(function () {
                    if (chosenXhr) {
                        chosenXhr.abort();
                    }
                    return chosenXhr = $.ajax(options);
                }, options.afterTypeDelay);
            });
        });
    };
})(jQuery);