angular.module('app').controller('addCommentsController', function ($scope, $filter, attendanceRepository, commonRepository) {
       
    $scope.attendanceDate = $filter('date')(Date.now(), 'dd-MMM-yyyy');

    $scope.saveComments = function () {
        if ($scope.attendanceDate && $scope.empID && $scope.comments && $scope.purpose && $scope.status) {
            attendanceRepository.saveComments($scope.comments, $scope.empID, $scope.attendanceDate, $scope.purpose, $scope.status).then(function (response) {
                if (response.data.isSuccess) {
                    toastr.success(response.data.message);
                    $scope.clearData();
                    $scope.loadDefault();
                } else {
                    toastr.error(response.data.message);
                }
            })
        } else {
            toastr.error('Please Fillup All Required Field');
        }
        
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
        $scope.purposeList = commonRepository.getPurpose();
        $scope.loadAttendance();
    }

    $scope.loadAttendance = function () {
        debugger
        var buyers = attendanceRepository.loadComments($scope.empID, $scope.attendanceDate).then(function (response) {
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
                    // get out time from outTime dateTime field
                    if (response.data[i].OutTime) {
                        var outDateTime = response.data[i].OutTime.replace('/Date(', '').replace(')/', '');
                        outDateTime = $filter('date')(outDateTime, "dd/MM/yyyy hh:mm a");
                        time = outDateTime.split(" ");

                        time = time[1] + " " + time[2];
                        response.data[i].OutTime = time;
                    }
                    // get out time from Last InTime dateTime field
                    
                }
                $scope.attendanceList = response.data;

                // Organize date wise data
                $scope.organizedDateWiseDate(response);
            }
        });
    }

    $scope.organizedDateWiseDate = function (response) {
        $scope.attendanceList = [];
        $scope.attendance = {};
        $scope.attendance.InTime = '';
        $scope.attendanceList.push($scope.attendance);

        $scope.attendance = {};
        $scope.attendance.EmpName = 'NAME';
        $scope.attendance.InTime = 'IN';
        $scope.attendance.OutTime = 'OUT';
       
        $scope.attendance.Comments = 'Comment';
        $scope.attendance.LateStatus = 'Late Status';
       
        $scope.attendanceList.push($scope.attendance);

        $scope.previousDate = $scope.attendanceDate;


        for (i = 0; i < response.data.length; i++) {

            debugger
            $scope.attendance = {};

            $scope.attendance.EmpID = response.data[i].EmpID;
            $scope.attendance.EmpName = response.data[i].EmpName;
            $scope.attendance.InTime = response.data[i].InTime;
            $scope.attendance.OutTime = response.data[i].OutTime;
            $scope.attendance.Comments = response.data[i].Comments;
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
                    if (response.data[i].InsideOfficeDuration != null) {
                        response.data[i].InsideOfficeDuration = response.data[i].InsideOfficeDuration.Hours + ':' + response.data[i].InsideOfficeDuration.Minutes + ':' + response.data[i].InsideOfficeDuration.Seconds;
                    }

                    if (response.data[i].OutsideOfficeDuration != null) {
                        response.data[i].OutsideOfficeDuration = response.data[i].OutsideOfficeDuration.Hours + ':' + response.data[i].OutsideOfficeDuration.Minutes + ':' + response.data[i].OutsideOfficeDuration.Seconds;
                    }
                }
                $scope.detailsEmpList = response.data;
            }
        })
    }

    $scope.clearData = function () {       
        $scope.comments = '';
        $scope.empID = '';
        $scope.attendanceDate = '';
        $scope.attendanceDate = $filter('date')(Date.now(), 'dd-MMM-yyyy');
    }

})