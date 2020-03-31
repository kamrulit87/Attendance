angular.module('app').controller('attendanceController', function ($scope, $filter, attendanceRepository, commonRepository) {
    
    $scope.attendance = {}
    $scope.fromDate = $filter('date')(Date.now(), 'dd-MMM-yyyy');
    $scope.toDate = $filter('date')(Date.now(), 'dd-MMM-yyyy');
    $scope.message = 'ok';
   

    $scope.loadDefault = function () {  
        var employee = attendanceRepository.loadEmployee().then(function (response) {
            if (response.data) {
                $scope.empList = response.data;
            }
        });

        $scope.lateStatusList = commonRepository.getStatus();
        $scope.status = "A";
        $scope.loadAttendance();
    }

    //$scope.changeDate = function () {
    //    alert($scope.fromDate);
    //}

    $scope.getUpdate = function () {
        debugger
        attendanceRepository.getUpdateData().then(function (response){
            if (response.data) {
                $scope.loadAttendance();
                debugger
                $scope.message = response.data
            }           
        })
    }

    $scope.loadAttendance = function () {
        debugger
        var buyers = attendanceRepository.loadAttendance($scope.empID, $scope.status, $scope.fromDate, $scope.toDate).then(function (response) {
            if (response.data) {                

                for (i = 0; i < response.data.length; i++) {
                    if (response.data[i].InTime) {                        
                        // get in time from inTime dateTime field
                        var inDateTime = response.data[i].InTime.replace('/Date(', '').replace(')/', '');
                        inDateTime = $filter('date')(inDateTime, "dd/MM/yyyy hh:mm a");//hh:mm:ss Z': 'utc'
                        //inDateTime = $filter('date')(inDateTime, "hh:mm a");                        
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
                    if (response.data[i].LastInTime) {
                        var lastInTimeDateTime = response.data[i].LastInTime.replace('/Date(', '').replace(')/', '');
                        lastInTimeDateTime = $filter('date')(lastInTimeDateTime, "dd/MM/yyyy hh:mm a");
                        time = lastInTimeDateTime.split(" ");

                        time = time[1] + " " + time[2];
                        response.data[i].LastInTime = time;
                    }
                    // set in out status                    
                    if (response.data[i].OfficeStatus) {
                        var status = 'Inside';
                        if (response.data[i].OfficeStatus == 'O') {
                            status = 'Outside';
                        }
                        response.data[i].OfficeStatus = status;
                    }
                }
                //$scope.attendanceList = response.data;

                // Organize date wise data
                $scope.organizedDateWiseDate(response);
            }
        });
    }

    $scope.organizedDateWiseDate = function (response) {
        $scope.attendanceList = [];
        $scope.previousDate = '';

        for (i = 0; i < response.data.length; i++) {

            if ($scope.previousDate !== response.data[i].WDate) {
                $scope.attendance = {};                
                $scope.attendance.EmpName = response.data[i].WDate;
                $scope.attendanceList.push($scope.attendance);

                $scope.attendance = {};
                $scope.attendance.EmpName = 'NAME';
                $scope.attendance.InTime = 'IN';
                $scope.attendance.OutTime = 'OUT';
                $scope.attendance.LastInTime = 'LAST IN';
                $scope.attendance.OfficeStatus = 'STATUS';
                $scope.attendance.WorkingHour = 'W.HOURS';
                $scope.attendance.LateStatus = 'Late Status';
                $scope.attendanceList.push($scope.attendance);
                $scope.previousDate = response.data[i].WDate;
            }


            $scope.attendance = {};
            $scope.attendance.EmpID = response.data[i].EmpID;
            $scope.attendance.EmpName = response.data[i].EmpName;
            $scope.attendance.InTime = response.data[i].InTime;
            $scope.attendance.OutTime = response.data[i].OutTime;
            $scope.attendance.LastInTime = response.data[i].LastInTime;
            $scope.attendance.OfficeStatus = response.data[i].OfficeStatus;
            $scope.attendance.WorkingHour = response.data[i].WorkingHour;
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
                    if (response.data[i].InTime !=null) {
                        // get in time from inTime dateTime field
                        var inDateTime = response.data[i].InTime.replace('/Date(', '').replace(')/', '');
                        inDateTime = $filter('date')(inDateTime, "dd/MM/yyyy hh:mm a");
                        time = inDateTime.split(" ");

                        time = time[1] + " " + time[2];
                        response.data[i].InTime = time;
                    }
                    // get out time from outTime dateTime field
                    if (response.data[i].OutTime !=null) {
                        var outDateTime = response.data[i].OutTime.replace('/Date(', '').replace(')/', '');
                        outDateTime = $filter('date')(outDateTime, "dd/MM/yyyy hh:mm a");
                        time = outDateTime.split(" ");

                        time = time[1] + " " + time[2];
                        response.data[i].OutTime = time;
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
            }
        })
    }

    $scope.clearData = function () {
        //for clear all field
        $scope.attendance = {};
        $scope.fromDate = $filter('date')(Date.now(), 'dd-MMM-yyyy');
        $scope.toDate = $filter('date')(Date.now(), 'dd-MMM-yyyy');
    }

    $scope.viewDailyAttendanceReport = function () {
        var reportUrl = '/AttendanceReports/Reports/LoadAttendance';
        var parameters = { EmpID: $scope.empID, Status: $scope.status, FromDate: $scope.fromDate, ToDate: $scope.toDate };
        printBaseOnMultiParameter(reportUrl, parameters, 'Do you want to view this Report ?', '')

        //if ($scope.status != null) {
        //    var reportUrl = '/AttendanceReports/Reports/LoadAttendance';
        //    var parameters = { EmpID: $scope.empID, Status: $scope.status, FromDate: $scope.fromDate, ToDate: $scope.toDate };
        //    printBaseOnMultiParameter(reportUrl, parameters, 'Do you want to view this Report ?', '')
        //}
        //else {
        //    toastr.error('Status Field is Required !!');
        //}
    }
})