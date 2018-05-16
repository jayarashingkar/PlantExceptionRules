// define the columns in your datasource
var columns = [
     {
         label: 'Rule',
         property: 'ExceptionID',
         sortable: true,
         width: '40px' 
     },
     {
         label: 'Specifications',
         property: 'Spec',
         sortable: true,
         width: '200px'
     },
      {
          label: 'SpecRev',
          property: 'SpecRev',
          sortable: true,
          width: '30px'
      },
    
    {
        label: 'Alloy',
        property: 'Alloy',
        sortable: true,
        width: '50px'
    },
    {
        label: 'Temper',
        property: 'Temper',
        sortable: true,
        width: '50px'
    },
    {
        label: 'MinSecThick',
        property: 'MinSecThick',
        width: '40px'
    },
    {
        label: 'MaxSecThick',
        property: 'MaxSecThick',
        width: '40px'
    },
    {
        label: 'CustPart',
        property: 'CustPart',
        width: '50px'
    },
    {
        label: 'UACPart',
        property: 'UACPart',
        width: '50px'
    },
    {
        label: 'Plant',
        property: 'PlantDescription',
        sortable: true,
        width: '50px'
    },
    //{
    //    label: 'Severity',
    //    property: 'Severity',
    //    width: '50px'
    //},
    {
        label: 'Approval',
        property: 'Approval',
        width: '50px'
    },
     {
         label: 'RuleTurnedOn',
         property: 'Enabled',
         width: '30px'
     },
    //{
    //    label: 'Enabled',
    //    property: 'Enabled',
    //    width: '30px'
    //},
    {
        label: 'Note',
        property: 'Note',
      //  width: '50px'
    },
];

function customColumnRenderer(helpers, callback) {
    // determine what column is being rendered
    var column = helpers.columnAttr;

    // get all the data for the entire row
    var rowData = helpers.rowData;
    var customMarkup = '';
 
    list_columnSizing = true;
    list_highlightSortedColumn = true;

    // only override the output for specific columns.
    // will default to output the text value of the row item
    switch (column) {
        case 'ExceptionID':
            // let's combine name and description into a single column      
            customMarkup = '<div style="font-size:12px;">' + rowData.ExceptionID + '</div>';
            break;       
        default:
            // otherwise, just use the existing text value
            customMarkup = helpers.item.text();
            break;
    }

    helpers.item.html(customMarkup);

    callback();
}

function customRowRenderer(helpers, callback) {
    // let's get the id and add it to the "tr" DOM element
    var item = helpers.item;
    //  item.attr('id', 'row' + helpers.rowData.RecId);
    item.attr('id', 'row' + helpers.rowData.ExceptionID);

    callback();
}

// this example uses an API to fetch its datasource.
// the API handles filtering, sorting, searching, etc.
function customDataSource(options, callback) {
    // set options   

    var pageIndex = options.pageIndex;
    var pageSize = options.pageSize;
     var search = '';


    if ($('#Spec').val())
        search += ';' + 'Spec:' + $('#Spec').val();
    if ($('#Alloy').val())
        search += ';' + 'Alloy:' + $('#Alloy').val();
    if ($('#Temper').val())
        search += ';' + 'Temper:' + $('#Temper').val();
    if ($('#Plant').val())
        search += ';' + 'Plant:' + $('#Plant').val();

    var dataoptions = {
        pageIndex: pageIndex,
        pageSize: pageSize,      
        sortBy: (typeof options.sortProperty != 'undefined') ? options.sortProperty : "",
        sortDirection: (typeof options.sortDirection != 'undefined') ? options.sortDirection : "",
        filterBy: options.filter.value || '',
        searchBy: search || ''
    };

    $.ajax({
        type: 'post',
        url: '../Grid/GetExceptionList',
        data: dataoptions
    })       
        .done(function (data) {          
            if (data) {               
                if (data.total != 0) {
                    var items = data.items;
                    var totalItems = data.total;
                    var totalPages = Math.ceil(totalItems / pageSize);
                    var startIndex = (pageIndex * pageSize) + 1;
                    var endIndex = (startIndex + pageSize) - 1;

                    if (items) {
                        if (endIndex > items.length) {
                            endIndex = items.length;
                        }
                    }
                    // configure datasource
                    var dataSource = {
                        page: pageIndex,
                        pages: totalPages,
                        count: totalItems,
                        start: startIndex,
                        end: endIndex,
                        columns: columns,
                        items: items
                    };

                    // invoke callback to render repeater
                    callback(dataSource);
                }
                else {
                   // debugger;
                    var message = data.Message;
                    bootbox.alert(message);              
                }
            }
            else {               
                bootbox.alert("no data");              
            }
        });


}

$('#btnSearch').on('click', function () {
    $('#ExceptionRepeater').repeater('render');
    return false;
});

$('#btnClear').on('click', function () {
    //check if this should be drop down menu - currently keep text for search
    $('#Spec').val('');
    $('#Alloy').val('');
    $('#Temper').val('');
    $('#Plant').val('');
    //$('#UACPart').val('');
    //$('#CustPart').val('');
    //$('#Severity').val('');
    $('#ExceptionRepeater').repeater('render');
    return false;
});

$(document).ready(function () {
    //   debugger;
    $('.selectpicker').selectpicker();
    $('#Spec').val('');
    $('#Alloy').val('');
    $('#Temper').val('');
    $('#Plant').val('');
    //$('#UACPart').val('');
    //$('#CustPart').val(''); 
    //$('#Severity').val('');


});