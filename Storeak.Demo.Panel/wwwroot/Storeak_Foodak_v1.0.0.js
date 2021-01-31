function OpenModal(URL) {
    showLoader();
    $(".modal-content").load(URL, function () {
        $(".select").select2();
        $('#Modal').modal({
            backdrop: 'static'
        });
    });
}

var ModalSuccessAlert = function (data, alertDivId = 'alert') {
    if (data.success !== undefined) {
        $('#' + alertDivId).removeAttr("class");
        $('#' + alertDivId).removeAttr("style");
        $('#' + alertDivId + ' div').removeAttr("style");
        $('#' + alertDivId + ' div').attr('style', 'font-size:larger;font-weight:bold;');
        $('#' + alertDivId + ' div').text(data.message);
        if (data.success) {
            $('#' + alertDivId).addClass("alert alert-success");
            setTimeout("$('#" + alertDivId + "').toggle();hideModal();", 2000);
        }
        else {
            $('#' + alertDivId).addClass("alert alert-danger");
            setTimeout("$('#" + alertDivId + "').toggle()", 2000);
        }
    }
    else {
        $('.modal-content').html(data);
        $(".select").select2();
    }
};

var SuccessAlert = function (data, redirectUrl, alertDivId = 'global-alert') {
    if (data.success !== undefined) {
        $('#' + alertDivId).removeAttr("class");
        $('#' + alertDivId).removeAttr("style");
        $('#' + alertDivId).attr("style", 'margin-top:2%');
        $('#' + alertDivId + ' div').removeAttr("style");
        $('#' + alertDivId + ' div').attr('style', 'font-size:larger;font-weight:bold;');
        $('#' + alertDivId + ' div').text(data.message);
        if (data.success) {
            $('#' + alertDivId).addClass("alert alert-success");
            setTimeout("$('#" + alertDivId + "').toggle();location.href='" + redirectUrl + "'", 2000);
        }
        else {
            $('#' + alertDivId).addClass("alert alert-danger");
            setTimeout("$('#" + alertDivId + "').toggle()", 2000);
        }
    }
    else {
        $('#kt_content').html(data);
        $(".select").select2();
    }
}

var FailedAlert = function (data) {
    var errorHtml = $(data.responseText);
    if (errorHtml.find('#kt_content').length > 0) {
        $('#kt_content').html(errorHtml.find('#kt_content'));
    }
    else {
        $('html').html(errorHtml);
    }
};

var SuccessorFailedAlert = function (message, isSuccess, alertDivId = 'alert', redirectUrl = "") {
    $('#' + alertDivId).removeAttr("class");
    $('#' + alertDivId).removeAttr("style");
    $('#' + alertDivId).attr("style", 'margin-top:2%');
    $('#' + alertDivId + ' div').removeAttr("style");
    $('#' + alertDivId + ' div').attr('style', 'font-size:larger;font-weight:bold;');
    $('#' + alertDivId + ' div').text(message);
    if (isSuccess === "True") {
        $('#' + alertDivId).addClass("alert alert-success");
        if (redirectUrl !== "") {
            setTimeout("$('#" + alertDivId + "').toggle();location.href='" + redirectUrl + "'", 5000);
        }
        else {
            setTimeout("$('#" + alertDivId + "').toggle();", 5000);
        }
    }
    else {
        $('#' + alertDivId).addClass("alert alert-danger");
        setTimeout("$('#" + alertDivId + "').toggle()", 5000);
    }
};

var showLoader = function () {
    if ($('#Modal').is(':visible')) {
        KTApp.block('.modal-content', {
            overlayColor: '#000000',
            state: 'primary',
            message: 'Please wait...'
        });
    }
    else {
        KTApp.block('body', {
            overlayColor: '#000000',
            type: 'v2',
            state: 'primary',
            message: 'Please wait...'
        });
    }
};

var hideLoader = function () {
        KTApp.unblock('.modal-content');
        KTApp.unblock('body');
};

var hideModal = function () {
    $('.modal').modal('hide');
};

function ClearLocalStorage() {
    window.localStorage.setItem('MenuId', null);
}

function ClearPaginationLocalStorage() {
    window.localStorage.setItem('CurrentPage', null);
    window.localStorage.setItem('SortField', null);
    window.localStorage.setItem('CurrentSortField', null);
    window.localStorage.setItem('CurrentSortOrder', null);
    window.localStorage.setItem('NextSortOrder', null);
    window.localStorage.setItem('PageSize', null);
}

var url = "";
var tableDivId = "";
var paramterObject = null;
function LoadPagingTable(Url = null, TableDivId = null, ParamterObject = null, SortField = null, CurrentSortField = null, CurrentSortOrder = null, PageNo = null, PageSize = null) {
    var data = {
        "CurrentPage": PageNo === null ? window.localStorage.getItem('CurrentPage') : PageNo,
        "PageSize": PageSize === null ? window.localStorage.getItem('PageSize') : PageSize,
        "SortField": SortField === null ? null : SortField,
        "CurrentSortField": CurrentSortField === null ? null : CurrentSortField,
        "CurrentSortOrder": CurrentSortOrder === null ? null : CurrentSortOrder
    };
    
    if (Url !== null && Url !== "") {
        url = Url;
    }
    if (TableDivId !== null && TableDivId !== "") {
        tableDivId = TableDivId;
    }
    if (ParamterObject !== null) {
        paramterObject = ParamterObject;
    }
    if (paramterObject) {
        for (var propertyName in paramterObject) {
            var key = propertyName;
            var value = paramterObject[key];
            data[key] = value;
        }
    }
    $.ajax({
        url: url,
        dataType: "html",
        type: "GET",
        data: data,
        contentType: "application/json",
        success: function (response) {
            $('#' + tableDivId).html(response);
        },
        error: function (err) {
            FailedAlert(err);
        }
    });
}

function LoadTable(Url, TableDivId) {
    $.ajax({
        url: Url,
        dataType: "html",
        type: "GET",
        contentType: "application/json",
        success: function (response) {
            $('#' + TableDivId).html(response);
        },
        error: function (err) {
            FailedAlert(err);
        }
    });
}
function Reset(selector) {
    $(selector).find(':input').each(function () {
        if (this.type === 'submit') {
            //do nothing
        }
        else if (this.type === 'checkbox' || this.type === 'radio') {
            this.checked = false;
        }
        else if (this.type === 'file') {
            var control = $(this);
            control.replaceWith(control = control.clone(true));
        } else {
            $(this).val('');
        }
    });
    $(selector).find('select').each(function () {
        if ($(this).find('option').first().val() === '0') {
            $(this).val(0).trigger('change');
        }
        else {
            $(this).val('').trigger('change');
        }
    });
}