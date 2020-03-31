angular.module('app').controller('allEmpInController', function ($scope, $filter, attendanceRepository, commonRepository) {

    $scope.attendance = {}
    $scope.fromDate = $filter('date')(Date.now(), 'dd-MMM-yyyy');
    $scope.toDate = $filter('date')(Date.now(), 'dd-MMM-yyyy');

    $scope.loadDefault = function () {
        var employee = attendanceRepository.loadEmployee().then(function (response) {
            if (response.data) {
                $scope.empList = response.data;
            }
        });

        $scope.lateStatusList = commonRepository.getStatus();
        //$scope.loadAllInEmpAttendance();
    }

    $scope.loadAllInEmpAttendance = function () {
        debugger
        if ($scope.fromDate != null && $scope.toDate != null && $scope.empID != null) {
            var buyers = attendanceRepository.loadAllInEmpAttendance($scope.empID, $scope.status, $scope.fromDate, $scope.toDate).then(function (response) {
                if (response.data) {

                    for (i = 0; i < response.data.length; i++) {
                        if (response.data[i].InTime) {
                            // get in time from inTime dateTime field
                            var inDateTime = response.data[i].InTime.replace('/Date(', '').replace(')/', '');
                            inDateTime = $filter('date')(inDateTime, "dd/MM/yyyy hh:mm a");
                            time = inDateTime.split(" ");

                            time = time[1] + " " + time[2];
                            response.data[i].InTime = time;
                        }

                        if (response.data[i].WDate != null) {
                            var wDate = response.data[i].WDate.replace('/Date(', '').replace(')/', '');
                            response.data[i].WDate = $filter('date')(wDate, "dd-MMM-yyyy");
                        }
                    }
                    // Organize date wise data
                    $scope.organizedDateWiseDate(response);
                }
            });
        }
        else {
            toastr.error('Please Fill Up All The Required Field !!');
        }
    
    }

    $scope.organizedDateWiseDate = function (response) {
        $scope.attendanceList = [];
        $scope.attendance = {};
        $scope.attendance.InTime = '';
        $scope.attendanceList.push($scope.attendance);

        $scope.attendance = {};
        $scope.attendance.WDate = 'DATE'
        $scope.attendance.EmpName = 'NAME';
        $scope.attendance.InTime = 'IN';
        $scope.attendance.LateStatus = 'Late Status';
        $scope.attendanceList.push($scope.attendance);

        $scope.previousDate = $scope.toDate;


        for (i = 0; i < response.data.length; i++) {
            
            $scope.attendance = {};

            $scope.attendance.EmpID = response.data[i].EmpID;
            $scope.attendance.EmpName = response.data[i].EmpName;
            $scope.attendance.InTime = response.data[i].InTime;
            $scope.attendance.LateStatus = response.data[i].LateStatus;
            $scope.attendance.WDate = response.data[i].WDate;
            $scope.attendanceList.push($scope.attendance);
        }
    }


    $scope.loadDetailsEmp = function (row) {
        $scope.detailsEmpList = [];

        attendanceRepository.loadDetailsEmp(row.EmpID, row.WDate).then(function (response) {
            if (response.data) {
                for (i = 0; i < response.data.length; i++) {
                    if (response.data[i].InTime != null) {
                        // get in time from inTime dateTime field
                        var inDateTime = response.data[i].InTime.replace('/Date(', '').replace(')/', '');
                        inDateTime = $filter('date')(inDateTime, "dd/MM/yyyy hh:mm a");
                        time = inDateTime.split(" ");

                        time = time[1] + " " + time[2];
                        response.data[i].InTime = time;
                    }
                    
                    debugger
                    // convert 
                    if (response.data[i].InsideOfficeDuration != null) {
                        response.data[i].InsideOfficeDuration = response.data[i].InsideOfficeDuration.Hours + ':' + response.data[i].InsideOfficeDuration.Minutes + ':' + response.data[i].InsideOfficeDuration.Seconds;
                    }

                    if (response.data[i].OutsideOfficeDuration != null) {
                        response.data[i].OutsideOfficeDuration = response.data[i].OutsideOfficeDuration.Hours + ':' + response.data[i].OutsideOfficeDuration.Minutes + ':' + response.data[i].OutsideOfficeDuration.Seconds;
                    }


                }
                $scope.detailsEmpList = response.data;
                //alert(JSON.stringify(response.data))
            }
        })
    }

    $scope.clearData = function () {

        //for clear all field
        $scope.attendance = {};
        $scope.fromDate = $filter('date')(Date.now(), 'dd-MMM-yyyy');
        $scope.toDate = $filter('date')(Date.now(), 'dd-MMM-yyyy');
    }

})