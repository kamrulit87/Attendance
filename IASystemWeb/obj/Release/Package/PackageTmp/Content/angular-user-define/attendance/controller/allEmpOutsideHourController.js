angular.module('app').controller('allEmpOutsideHourController', function ($scope, $filter, attendanceRepository,commonRepository) {

    $scope.toDate = $filter('date')(Date.now(), 'dd-MMM-yyyy');
    
    $scope.loadDefault = function () {
        debugger
        $scope.empList = [];
        var employee = attendanceRepository.loadEmployee().then(function (response) {
            if (response.data) {
                $scope.empList = response.data;
            }
        });

        $scope.lateStatusList = commonRepository.getStatus();

        $scope.allEmpOutsideHour();
    }

    $scope.allEmpOutsideHour = function () {
        debugger
        var buyers = attendanceRepository.allEmpOutsideHour($scope.toDate).then(function (response) {
            if (response.data != null) {
                $scope.attendanceList = response.data;
            }
        });
    }

    $scope.clearData = function () {
        $scope.empID = '';
        $scope.toDate = '';
        $scope.toDate = $filter('date')(Date.now(), 'dd-MMM-yyyy');
    }

    $scope.viewDailyOutsideHourReport = function () {
        var reportUrl = '/AttendanceReports/Reports/EmpOutsideHour';
        debugger
        var parameters = { ToDate: $scope.toDate };
        printBaseOnMultiParameter(reportUrl, parameters, 'Do you want to view this Report ?', '')
    }
})