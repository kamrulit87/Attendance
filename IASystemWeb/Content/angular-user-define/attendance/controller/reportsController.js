angular.module('app').controller('reportsController', function ($scope, $filter, attendanceRepository, commonRepository) {
    $scope.loadDefault = function () { 
        $scope.monthList = commonRepository.getMonthWithDays();
    }

    //$scope.fromDate = $filter('date')(Date.now(), 'dd-MMM-yyyy');
    //$scope.toDate = $filter('date')(Date.now(), 'dd-MMM-yyyy');

        $scope.monthlyLateCountReport = function () {
            debugger
            //if ($scope.toDate < $scope.fromDate) {
               // toastr.warning("To Date Naver Be Lessthen From Date");
            //}

            //else {
            $scope.month=[];

            $scope.month=commonRepository.getMonthWithDays();
            var month = $filter('filter')($scope.month, function (d) { return d.monthID === $scope.monthID; })[0];
            $scope.month = month.label;

                var reportUrl = '/AttendanceReports/Reports/LoadMonthlyLateCount';
                var parameters = { monthID: $scope.monthID, monthName: $scope.month };
                printBaseOnMultiParameter(reportUrl, parameters, 'Do you want to view this Report ?', '')
            //}
       
        
    }
})