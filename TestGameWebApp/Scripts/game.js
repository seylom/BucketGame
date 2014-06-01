/**
*A simple proxy to the actual jquery function
*/
function serviceProxy(serviceUrl) {
    var _I = this;
    this.serviceUrl = serviceUrl;

    /**
    *A simple wrapper function jquery's ajax method for further processing
    */
    this.invoke = function (method, data, callback, error, verb) {
        if (typeof verb == undefined) {
            verb = "POST";
        }

        var url = _I.serviceUrl + method;
        $.ajax({
            url: url,
            data: data,
            type: verb,
            timeout: 10000,
            dataType: "json",
            success: callback,
            error: function (xhr) {
                if (!error)
                    return;

                if (xhr.responseText) {
                    var err = JSON.parse(xhr.responseText);
                    if (err)
                        error(err);
                    else
                        error({ Message: "Unknown server error." });
                }
                return;
            }
        });
    }
}

/**
*A wrapper function for everything from game input validation to results display
*/
var game_steps = function () {
    var Proxy;
    var result_box = $("#results_bx");
    var entry_bx = $("#entry_bx");

    /**
    *Initialize this object
    */
    var Initialize = function () {
        setup_interaction();
    };


    /**
    *Setup for event binding and callback as well as ajax calls
    */
    function setup_interaction() {
        $("#solve_btn").click(function (e) {

            reset_validation();

            var val1 = $("#bucket1").val();
            var val2 = $("#bucket2").val();
            var val3 = $("#target_val").val();

            var val_result = validate_simple(val1, val2, val3);

            if (val_result > 0) {
                var DTO = { 'bucket1': val1, 'bucket2': val2, 'target': val3 };
                Proxy.invoke('', DTO, build_results, function () {
                    alert('Unable to retrieve results');
                }, 'GET');
            }
        });
    }

    /**
    *Performs input validation on fields provided for buckets and target value
    *@param {string} val1 : value for bucket1
    *@param {string} val2 : value for bucket1
    *@param {string} val3 : Target value requested
    */
    function validate_simple(val1, val2, val3) {
        var valid = 1;
        var intRegex = /^\d+$/;

        if (!val1) {
            $("#b1_valid").css('display', 'inline');
            $("#b1_valid_mess").html("Bucket 1 value is required");
            valid = 0;
        }
        else if (!intRegex.test(val1)) {
            $("#b1_valid").css('display', 'inline');
            $("#b1_valid_mess").html("Bucket 1 should be a positive integer");
            valid = 0;
        }

        if (!val2) {
            $("#b2_valid").css('display', 'inline');
            $("#b2_valid_mess").html("Bucket 2 value is required");
            valid = 0;
        }
        else if (!intRegex.test(val2)) {
            $("#b2_valid").css('display', 'inline');
            $("#b2_valid_mess").html("Bucket 1 should be a positive integer");
            valid = 0;
        }

        if (!val3) {
            $("#target_valid").css('display', 'inline');
            $("#target_valid_mess").html("Target value is required");
            valid = 0;
        }
        else if (!intRegex.test(val3)) {
            $("#target_valid").css('display', 'inline');
            $("#target_valid_mess").html("Target value should be a positive integer");
            valid = 0;
        }

        if ((val1 && val2 && val3) &&
            (parseInt(val3) > parseInt(val1)) &&
            (parseInt(val3) > parseInt(val2))) {
            $("#target_valid").css('display', 'inline');
            $("#target_valid_mess").html("Target should be smaller than both buckets");
            valid = 0;
        }

        return valid;
    }

    /**
    *Clears input validation messages
    */
    function reset_validation() {
        $('#result_steps').empty();
        $("#b1_valid").css('display', 'none');
        $("#b2_valid").css('display', 'none');
        $("#target_valid").css('display', 'none');
    }

    /**
    *Creates the solution output using the provided json string
    *@param {} res : the json string 
    */
    function build_results(res) {
        result = JSON.parse(res);

        $('#result_steps').empty();

        $('#results_bx').fadeOut("fast", function () {
            if (result) {

                if (result.Steps.length > 0) {
                    for (var i = 0; i < result.Steps.length; i++) {
                        add_stepItem(result.Steps[i], i + 1)
                    }
                }
                else {
                    $('<p id="no_sol"><b>No solutions found!</b></p>').appendTo('#result_steps');
                }

                displayResults();
            }
        });
    }

    /**
    *Adds a solution step to the result area
    *@param {object} item : the step item 
    *@param {Number} index : the index of t
    */
    function add_stepItem(item, index) {

        var stepItem = '<div class="trans">' +
                            '<div class="buck">' +
                                '<div class="rbuck"></div>' +
                                '<div class="capacity">' +
                                    item.EndState.Bucket1 +
                                '</div>' +
                            '</div>' +
                            '<div class="buck">' +
                                '<div class="wbuck"></div>' +
                                '<div class="capacity">' +
                                    item.EndState.Bucket2 +
                                '</div>' +
                            '</div>' +
                            '<div class="step_mess">' +
                                '<p><span class="stepnum">' + index + '</span>' + item.Details + '</p>' +
                            '</div>' +
                        '</div>"';

        $(stepItem).addClass('step').appendTo('#result_steps');
    }

    /**
    *Makes the result display section visible
    */
    var displayResults = function () {
        $('#results_bx').fadeIn("fast", function () { });
    };

    return {
        Init: function (proxy_url) {
            Proxy = new serviceProxy(proxy_url);
            Initialize();
        }
    }
} ();