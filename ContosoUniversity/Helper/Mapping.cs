using AutoMapper;
using ContosoUniversity.Entity;
using ContosoUniversity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContosoUniversity.Web.Helper
{
    public class Mapping
    {
        public readonly IMapper _mapper;
        public Mapping(IMapper mapper)
        {
            _mapper = mapper;

        }

        public  StudentModel studentToStudentModel(Student studentEntity)
        {
            var studentModel =  _mapper.Map<StudentModel>(studentEntity);
            return  studentModel;
        }
        public  Student studentModelToStudent(StudentModel studentModel)
        {
            var studentEntity =  _mapper.Map<Student>(studentModel);
            return studentEntity;
        }
        public List<StudentModel> studentsToListStudent(IEnumerable <Student> studentEntity)
        {
            var studentModel = _mapper.Map<List<StudentModel>>(studentEntity);
            return studentModel;
        }
        public List<CourseModel> coursessToListCourses(IEnumerable<Course> courseEntity)
        {
            var courseModel = _mapper.Map<List<CourseModel>>(courseEntity);
            return courseModel;
        }
        public Course courseModelToCourse(CourseModel courseModel)
        {
            var courseEntity = _mapper.Map<Course>(courseModel);
            return courseEntity;

        }
        public CourseModel courseToCourseModel(Course courseEntity)
        {
            var courseModel = _mapper.Map<CourseModel>(courseEntity);
            return courseModel;

        }
        public Enrollment enrollmentModelToEnrollment(EnrollmentModel enrollmentModel)
        {
            var enrollmentEntity = _mapper.Map<Enrollment>(enrollmentModel);
            return enrollmentEntity;
        }
        
        public EnrollmentModel enrollmentToEnrollmentModel(Enrollment enrollmentEntity)
        {
            var enrollmentModel = _mapper.Map<EnrollmentModel>(enrollmentEntity);
            return enrollmentModel;
        }
        public List<EnrollmentModel> EntollmentToListEnrollment(IEnumerable<Enrollment> enrollmentEntity)
        {
            var enrollmentModel = _mapper.Map<List<EnrollmentModel>>(enrollmentEntity);
            return enrollmentModel;
        }
    }
}
