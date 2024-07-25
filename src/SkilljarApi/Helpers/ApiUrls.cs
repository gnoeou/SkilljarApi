using SkilljarApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SkilljarApi.Helpers
{
    public static partial class ApiUrls
    {

        #region Assests URLS
        internal static Uri Asset()
        {
            return "assets".FormatUri();
        }

        internal static Uri Asset(string assetId)
        {
            return "assets/{0}".FormatUri(assetId);
        }

        internal static Uri Assets(int pageSize = 25)
        {
            return "assets?page_size={0}".FormatUri(pageSize);
        }
        #endregion

        #region Domains URLs
        public static Uri Domains()
        {
            return "domains".FormatUri();
        }

        public static Uri Domain(string domainName)
        {
            return "domains/{0}".FormatUri(domainName);
        }

        internal static Uri AccessCodePools(string domainName)
        {
            return "domains/{0}/access-code-pools".FormatUri(domainName);
        }

        internal static Uri AccessCodePool(string domainName, string accessCodePoolId)
        {
            return "domains/{0}/access-code-pools/{1}".FormatUri(domainName, accessCodePoolId);
        }

        internal static Uri AccessCode(string domainName, string accessPoolId, string accessCodeId)
        {
            return "domains/{0}/access-code-pools/{1}/access-codes/{2}".FormatUri(domainName, accessPoolId, accessCodeId);
        }

        internal static Uri AccessCodes(string domainName, string accessPoolId)
        {
            return "domains/{0}/access-code-pools/{1}/access-codes".FormatUri(domainName, accessPoolId);
        }
        internal static Uri CourseSeries(string domainName, string courseSeriesId)
        {
            return "domains/{0}/course-series/{1}".FormatUri(domainName, courseSeriesId);
        }

        internal static Uri CourseSeries(string domainName)
        {
            return "domains/{0}/course-series".FormatUri(domainName);
        }

        internal static Uri CourseSeriesPublishedCourses(string domainName, string courseSeriesId)
        {
            return "domains/{0}/course-series/{1}/published-courses".FormatUri(domainName, courseSeriesId);
        }

        internal static Uri Plans(string domainName)
        {
            return "domains/{0}/plan".FormatUri(domainName);
        }
        internal static Uri Plan(string domainName, string planId)
        {
            return "domains/{0}/plan/{1}".FormatUri(domainName,planId);
        }

        internal static Uri PlanEnrollments(string domainName, string planId)
        { 
            return "domains/{0}/plan/{1}/plan-enrollments".FormatUri(domainName,planId);
        }
        internal static Uri PlanEnrollment(string domainName, string planId, string planEnrollmentId)
        {
            return "domains/{0}/plan/{1}/plan-enrollments/{2}".FormatUri(domainName, planId, planEnrollmentId);
        }

        public static Uri PublishedCourse(string domainName, string publishedCourseId)
        {
            return "domains/{0}/published-courses/{1}".FormatUri(domainName, publishedCourseId);
        }

        internal static Uri PublishedCourses(string domainName)
        {
            return "domains/{0}/published-courses?include_searchable_content=true".FormatUri(domainName);
        }

        internal static Uri PublishedCourseEnrollments(string domainName, string publishedCourseId)
        { 
            return "domains/{0}/published-courses/{1}/enrollments".FormatUri(domainName, publishedCourseId);
        }
        internal static Uri PublishedCourseEnrollments(string domainName, string publishedCourseId, int pageSize = 1000)
        {
            return "domains/{0}/published-courses/{1}/enrollments?page_size={2}".FormatUri(domainName, publishedCourseId,pageSize);
        }
        internal static Uri PublishedCourseEnrollment(string domainName, string publishedCourseId, string enrollmentId)
        {
            return "domains/{0}/published-courses/{1}/enrollments/{2}".FormatUri(domainName, publishedCourseId,enrollmentId);
        }

        public static Uri PublishedPath(string domainName, string publishedPathId)
        {
            return "domains/{0}/published-paths/{1}".FormatUri(domainName, publishedPathId);
        }

        internal static Uri PublishedPaths(string domainName)
        {
            return "domains/{0}/published-paths?include_searchable_content=true".FormatUri(domainName);
        }

        internal static Uri PublishedPathEnrollments(string domainName, string publishedPathid, int pageSize = 1000)
        {
            return "domains/{0}/published-paths/{1}/published-path-enrollments?page_size={2}".FormatUri(domainName,publishedPathid,pageSize);
        }

        internal static Uri PublishedPathEnrollment(string domainName, string publishedPathid, string enrollmentId) 
        {
            return "domains/{0}/published-paths/{1}/published-path-enrollments/{2}".FormatUri(domainName, publishedPathid, enrollmentId);
        }

        internal static Uri SignUpFields(string domainName)
        {
            return "domains/{0}/signup-fields".FormatUri(domainName);
        }

        internal static Uri Users(string domainName,int pageSize = 1000)
        {
            //TODO: Add support for addtional query parameters to filter the user list. https://api.skilljar.com/docs/#domains-users-list
            return "domains/{0}/users?page_size={1}".FormatUri(domainName,pageSize);
        }
        internal static Uri User(string domainName, string userId)
        {
            return "domains/{0}/users/{1}".FormatUri(domainName, userId);
        }

        internal static Uri UserInvites(string domainName, string userId)
        { 
            return "domains/{0}/users/{1}/invites".FormatUri(domainName,userId);
        }

        internal static Uri UserInvite(string domainName, string userId,string domainUserInviteId)
        {
            return "domains/{0}/users/{1}/invites/{2}".FormatUri(domainName, userId, domainUserInviteId);
        }

        internal static Uri UserSignUpFields(string domainName, string userId)
        {
            return "domains/{0}/users/{1}/signup-fields".FormatUri(domainName, userId);
        }
        internal static Uri UserSignUpField(string domainName, string userId,string userSignUpFieldId)
        {
            return "domains/{0}/users/{1}/signup-fields/{2}".FormatUri(domainName, userId, userSignUpFieldId);
        }
        internal static Uri UserSignUpFieldsBulk(string domainName, string userId)
        {
            return "domains/{0}/users/{1}/signup-fields-bulk".FormatUri(domainName, userId);
        }
        #endregion

        #region Courses URLs
        public static Uri Course()
        {
            return "courses/".FormatUri();
        }
        public static Uri Course(string courseId)
        {
            return "courses/{0}".FormatUri(courseId);
        }
        public static Uri Courses(int page_size)
        { 
            return "courses?page_size={0}".FormatUri(page_size);
        }
        #endregion


        internal static Uri Ping()
        {
            return "ping".FormatUri();
        }



    }
}
