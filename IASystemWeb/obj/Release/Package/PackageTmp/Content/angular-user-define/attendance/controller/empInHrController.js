angular.module('app').controller('empInHrController', function ($scope, $filter, attendanceRepository, commonRepository) {

    $scope.toDate = $filter('date')(Date.now(), 'dd-MMM-yyyy');
    $scope.EmpInsideHr = function () {
        $scope.empInHr = [];
    }
    $scope.loadDefault = function () {
        debugger
        $scope.empList = [];
        var employee = attendanceRepository.loadEmployee().then(function (response) {
            if (response.data) {
                $scope.empList = response.data;
            }
        });

        $scope.lateStatusList = commonRepository.getStatus();
        
        $scope.allEmpInHour();
    }
    
    $scope.allEmpInHour = function () {
        debugger
        var buyers = attendanceRepository.allEmpInHour($scope.toDate).then(function (response) {
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

    $scope.viewDailyInsideHourReport = function () {
        var reportUrl = '/AttendanceReports/Reports/EmpInsideHour';
        debugger
        var parameters = { ToDate: $scope.toDate };
        printBaseOnMultiParameter(reportUrl, parameters, 'Do you want to view this Report ?', '')
    }
})