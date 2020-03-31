angular.module('app').factory('commonRepository', function ($http) {
    return {

        getStatus: function () {
            var status = [{
                StatusID: 'A',
                label: 'All'
            }, {
                StatusID: 'L',
                label: 'Late'
            }, {
                StatusID: 'R',
                label: 'Regular'
            }];
            return status;
        },
        getPurpose: function () {
            var Purpose = [{
                PurposeID: 'O',
                label: 'Official'
            }, {
                PurposeID: 'P',
                label: 'Personal'
            }];
            return Purpose;
        },

        getMonthWithDays: function () {
            var month = [{
                monthID: '1',
                label: 'January'
            }, {
                monthID: '2',
                label: 'February'
            }, {
                monthID: '3',
                label: 'March'
            }, {
                monthID: '4',
                label: 'April'
            }, {
                monthID: '5',
                label: 'May'
            }, {
                monthID: '6',
                label: 'Jun'
            }, {
                monthID: '7',
                label: 'July'
            }, {
                monthID: '8',
                label: 'August'
            }, {
                monthID: '9',
                label: 'September'
            }, {
                monthID: '10',
                label: 'October'
            }, {
                monthID: '11',
                label: 'November'
            }, {
                monthID: '12',
                label: 'December'
            }];
            return month;
        },

        viewDocument: function (id, alertMessage) {
            var confirmatonMsg = alertMessage;
            var reportUrl = '/Reports/BoqReports/ViewBoqDtlsReport/';
            var transactionID = id;
            printDocument(reportUrl, transactionID, confirmatonMsg); //function location asset/custom.js       
        }
       
    }
})