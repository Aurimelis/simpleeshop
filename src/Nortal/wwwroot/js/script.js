$(function () {

    var checkboxes = $('.filters input[type=checkbox]');
    var tempHash;

    $('#clear_filters').click(function (e) {
        $(checkboxes).prop('checked', false);
        window.location.hash = '#';
    });

    $('#product-details-container').on('click', '.single-product', function (e) {
        if ($(this).hasClass('visible')) {

            var clicked = $(e.target);

            if (clicked.hasClass('close') || clicked.hasClass('overlay')) {
                if (tempHash) {
                    window.location.hash = tempHash;
                } else {
                    window.location.hash = '#';
                }
            }
        }
    });

    $(window).on('hashchange', function () {
        render(window.location.hash);
    });

    $(window).trigger('hashchange');

    $(".all-products").on('click', '.product-details-link', function (e) {
        var id = $(e.target).data('id');
        tempHash = window.location.hash;
        window.location.hash = "product/" + id;
    });

    $('.filter-criteria input[type=checkbox]').click(function (e) {
        var selectedCheckboxes = $('.filter-criteria input[type=checkbox]:checked');

        var filterHash = 'filter/';

        if (selectedCheckboxes.length > 0) {
            selectedCheckboxes.each(function () {
                if (filterHash.indexOf(this.name) < 0) {
                    filterHash = filterHash + this.name + '=' + this.value + '&';
                } else {
                    filterHash = insertIntoString(filterHash,
                        filterHash.indexOf(this.name) + this.name.length + 1,
                        this.value + '+');
                }
            });

            filterHash = filterHash.slice(0, -1);

            window.location.hash = filterHash;
        } else {
            window.location.hash = '#';
        }
    });

    function insertIntoString(str, index, value) {
        return str.substr(0, index) + value + str.substr(index);
    }

    function formatQsFromObj(filters) {
        var qs = '';
        for (var key in filters) {
            if (filters.hasOwnProperty(key)) {
                if (Array.isArray(filters[key])) {
                    for (var i = 0; i < filters[key].length; i++) {
                        qs = qs + encodeURIComponent(key) + "=" + encodeURIComponent(filters[key][i]) + "&";
                    }
                }
            }
        }

        qs = qs.slice(0, -1);

        return qs;
    }

    function loadProducts(filters) {
        $.get("/Home/ProductList", formatQsFromObj(filters), function (data) {
            $('#product-catalog').html(data);
            $('.all-products').addClass('visible');
        }).fail(function (error) {
            $('#product-catalog').html("<h3>" + error.status + "</h3><p>" + error.statusText + "</p>");
            $('.all-products').addClass('visible');
        });
    }

    function loadProduct(id) {
        $.get("/Home/ProductDetails", { productId: id }, function (data) {
            $('#product-details-container .preview-large').html(data);
            $("#product-details-container .single-product").addClass('visible');
        }).fail(function (error) {
            $('#product-details-container .preview-large').html("<h3>" + error.status + "</h3><p>" + error.statusText + "</p>");
            $("#product-details-container .single-product").addClass('visible');
        });
    }

    function matchFiltersToCheckboxes(filterObject) {
        $('.filters input[type=checkbox]').prop('checked', false);

        for (var key in filterObject) {
            if (filterObject.hasOwnProperty(key)) {
                if (Array.isArray(filterObject[key])) {
                    for (var i = 0; i < filterObject[key].length; i++) {
                        $('.filter-criteria input[name="' + key + '"][value="' + filterObject[key][i] + '"]')
                            .prop('checked', true);
                    }
                }
            }
        }
    }

    function render(url) {

        var temp = url.split('/')[0];

        $('.main-content .page').removeClass('visible');

        var map = {
            '': function () {
                loadProducts();
            },
            '#product': function () {
                var index = url.split('product/')[1].trim();

                loadProduct(index);
            },
            '#filter': function () {
                var filterCondition = url.split('#filter/')[1].trim();
                var filterSpecsArray = filterCondition.split("&");
                var filterObject = {};
                for (var i = 0; i < filterSpecsArray.length; i++) {
                    var spec = filterSpecsArray[i];
                    var specName = spec.split("=")[0];
                    var specValues = spec.split("=")[1].split("+");
                    filterObject[specName] = specValues;
                }

                matchFiltersToCheckboxes(filterObject);
                loadProducts(filterObject);
            }
        };
        map[temp]();
    }

});