angular.module('app').factory('attendanceRepository', function ($http) {
    return {
        loadEmployee: function () {
            return $http.post('/Home/LoadEmployee');
        },

        loadAttendance: function (empID, status, fromDate, toDate) {
            return $http.post('/Home/LoadAttendance', { 'empID': empID, 'status': status, 'fromDate': fromDate, 'toDate': toDate });
           
        },
        loadInOutAttendance: function (empID, fromDate, toDate) {
            return $http.post('/EmpInOut/LoadInOutAttendance', { 'empID': empID, 'fromDate': fromDate, 'toDate': toDate });

        },

        loadDetailsEmp: function (empID, wDate) {
            return $http.post('/Home/LoadDetailsEmp', { 'empID': empID, 'wDate': wDate });
        },
        // add Comments  

        loadComments: function (empID, toDate) {
            return $http.post('/AddComments/LoadAttendance', { 'empID': empID, 'toDate': toDate });
        },

        saveComments: function (comments, empID, attendanceDate, purpose, status) {
            return $http.post('/AddComments/SaveComments', { 'comments': comments, 'empID': empID, 'purpose': purpose, 'attendanceDate': attendanceDate, 'status': status });
        },

        //all employee In time 
        loadAllInEmpAttendance: function (empID, status, fromDate, toDate) {
            return $http.post('/AllEmpIn/LoadAttendance', { 'empID': empID, 'status': status, 'fromDate': fromDate, 'toDate': toDate });

        },

        //all employee Out time 
        loadAllOutEmpAttendance: function (empID, status, fromDate, toDate) {
            return $http.post('/AllEmpOut/LoadAttendance', { 'empID': empID, 'status': status, 'fromDate': fromDate, 'toDate': toDate });

        },
        //All emp Inside hour
        allEmpInHour: function (toDate) {
            return $http.post('/EmpInHr/EmpInsideHour', { 'toDate': toDate});
        },
        //All emp Outside hour
        allEmpOutsideHour: function (toDate) {
            return $http.post('/AllEmpOutsideHour/AllEmpOutsideHour', { 'toDate': toDate });
        },

        getUpdateData: function () {
            return $http.post('/Home/DownloadFromDevice');
        },

    }
})