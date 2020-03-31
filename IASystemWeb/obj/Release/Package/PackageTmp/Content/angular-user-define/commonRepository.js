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

        viewDocument: function (id, alertMessage) {
            var confirmatonMsg = alertMessage;
            var reportUrl = '/Reports/BoqReports/ViewBoqDtlsReport/';
            var transactionID = id;
            printDocument(reportUrl, transactionID, confirmatonMsg); //function location asset/custom.js       
        }
       
    }
})