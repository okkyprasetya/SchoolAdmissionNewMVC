using SchoolAdmission.BLL.DTOs;
using SchoolAdmission.BLL.DTOs.ApplicantDTO;
using SchoolAdmission.BLL.DTOs.GeneralObjectDTO;
using SchoolAdmission.BLL.DTOs.VerificatorDTO;
using SchoolAdmission.BLL.Interfaces;
using SchoolAdmission.DAL;
using SchoolAdmission.DAL.BOs.ApplicantDatas;
using SchoolAdmission.DAL.BOs.Users;
using SchoolAdmission.DAL.Interfaces;
using System.Net.Http.Headers;
using System.Reflection.Emit;

namespace SchoolAdmission.BLL
{
    public class AdministratorBLL : IAdministrator, IUser, IVerificator
    {
        private readonly IAdministratorDAL _administratorDAL;

        public AdministratorBLL()
        {
            _administratorDAL = new AdministratorDAL();
        }
        public void addVerificator(CreateVerificatorDTO entity)
        {
            try
            {
                var newVerificator = new VerificatorBO
                {
                    User = new DAL.BOs.UserBO
                    {
                        FirstName = entity.User.FirstName,
                        LastName = entity.User.LastName,
                        MiddleName = entity.User.MiddleName,
                        UserEmail = entity.User.UserEmail,
                        Password = entity.User.Password,
                    }
                };
                _administratorDAL.addVerificator(newVerificator);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error adding verificator: " + ex.Message);
            }

        }

        public void AssignBills()
        {
            try
            {
                _administratorDAL.AssignBills();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error assigning bills: " + ex.Message);
            }
        }

        public void completeVerificatorData(int verificatorID, string position, string SKNumber)
        {
            throw new NotImplementedException();
        }

        public void deleteApplicantData(int UserId)
        {
            throw new NotImplementedException();
        }

        public void deleteVerificator(int UserID)
        {
            try
            {
                _administratorDAL.deleteVerificator(UserID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error deleting verificator: " + ex.Message);
            }
        }

        public void finalizeLeaderboard(int quota)
        {
            try
            {
                _administratorDAL.finalizeLeaderboard(quota);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error finalizing leaderboard: " + ex.Message);
            }
        }

        public AcademicDataDTO getAcademicDataByID(int UGDataID)
        {
            AcademicDataDTO academicDataDTO = new AcademicDataDTO();
            var applicants = _administratorDAL.getAcademicDataByID(UGDataID);
            var academicsTarget = new AcademicDataDTO
            {
                UADataID = applicants.UADataID,
                RaportSummaries = applicants.RaportSummaries,
                RaportDocument= applicants.RaportDocument,
                isVerified= applicants.isVerified
            };

            return academicsTarget;
        }

        public List<AchievementRecordsDTO> getAchievementRecordsById(int UGDataID)
        {
            var records = _administratorDAL.GetAchievementRecords(UGDataID);
            List<AchievementRecordsDTO> achievementRecords = new List<AchievementRecordsDTO>();
            foreach (var record in records)
            {
                AchievementRecordsDTO achievementRecord = new AchievementRecordsDTO
                {
                    AchievementID = record.AchievementID,
                    Title = record.Title,
                    Description = record.Description,
                    Level = record.Level,
                    AchievementDocument = record.AchievementDocument,
                    isVerified = record.isVerified
                };
                if (achievementRecord.AchievementID != 0)
                {
                    achievementRecords.Add(achievementRecord);
                }
                else
                {
                    return achievementRecords;
                }
            }
            return achievementRecords;
        }

        public IEnumerable<UsersDTO> getAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicantsDTO> GetAllApplicants()
        {
            List<ApplicantsDTO> listApplicantDTO = new List<ApplicantsDTO>();
            try
            {
                var applicants = _administratorDAL.GetAllApplicants();
                foreach (var applicant in applicants)
                {
                    listApplicantDTO.Add(new ApplicantsDTO
                    {
                        UGDataID = applicant.UGDataID,
                        Users = new UsersDTO
                        {
                            FirstName = applicant.User.FirstName ?? string.Empty,
                            MiddleName = applicant.User.MiddleName ?? string.Empty,
                            LastName = applicant.User.LastName ?? string.Empty,
                        },
                        NIS = applicant.NIS,
                        Scholarship = new DTOs.GeneralObjectDTO.ScholarshipDataDTO
                        {
                            Name = applicant.Scholarship.Name ?? string.Empty,
                        },
                        isFinal = applicant.isFinal,
                        AcademicData = new AcademicDataDTO
                        {
                            UADataID = applicant.AcademicData.UADataID
                        },
                        PersonalData = new PersonalDataDTO
                        {
                            UPDataID = applicant.PersonalData.UPDataID
                        }
                    });
                }
                return listApplicantDTO;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error fetching data: " + ex.Message);
            }

        }

        public IEnumerable<VerificatorDTO> getAllVerificator()
        {
            var verificatorBOs = _administratorDAL.getAllVerificator();
            List<VerificatorDTO> listVerificatorDTO = new List<VerificatorDTO>();

            foreach (var verificatorBO in verificatorBOs)
            {
                VerificatorDTO verificatorDTO = new VerificatorDTO
                {
                    VerificatorID = verificatorBO.VerificatorID,
                    SKNumber = verificatorBO.SKNumber,
                    Position = verificatorBO.Position,
                    User = new UsersDTO
                    {
                        UserID = verificatorBO.User.UserID,
                        FirstName = verificatorBO.User.FirstName,
                        MiddleName = verificatorBO.User.MiddleName,
                        LastName = verificatorBO.User.LastName
                    },
                    Role = new RoleDTO
                    {
                        RoleId = verificatorBO.Role.RoleId,
                        RoleName = verificatorBO.Role.RoleName
                    }
                };

                listVerificatorDTO.Add(verificatorDTO);
            }

            return listVerificatorDTO;
        }

        public ApplicantsDTO getApplicantByID(int id)
        {
            throw new NotImplementedException();
        }

        public PersonalDataDTO getPersonalDataByID(int UGDataID)
        {
            PersonalDataDTO academicDataDTO = new PersonalDataDTO();
            var applicants = _administratorDAL.getPersonalDataByID(UGDataID);
            var personalsTarget = new PersonalDataDTO
            {
                UPDataID= applicants.UPDataID,
                FatherName = applicants.FatherName,
                FatherAddress=applicants.FatherAddress,
                FatherJob=applicants.FatherJob,
                FatherSalary=applicants.FatherSalary,
                MotherName = applicants.MotherName,
                MotherAddress = applicants.MotherAddress,
                MotherJob =applicants.MotherJob,
                MotherSalary = applicants.MotherSalary,
                SiblingsNumber = applicants.SiblingsNumber,
                Hobi= applicants.Hobi,
                KKDocument= applicants. KKDocument,
                BirthDocument = applicants. BirthDocument,
                isVerified= applicants. isVerified,
            };

            return personalsTarget;
        }

        public List<RankDTO> GetRank()
        {
            List<RankDTO> ranks = new List<RankDTO>();
            try
            {
                var Ranks = _administratorDAL.GetRank();
                foreach(var rank in Ranks)
                {
                    ranks.Add(new RankDTO
                    {
                        Rank = rank.Rank,
                        RegistrationID = rank.RegistrationID,
                        Name = rank.Name,
                        TotalScore = rank.TotalScore,
                        status = rank.status
                    });
                }
                return ranks;
            }catch (Exception ex)
            {
                throw new ApplicationException("Error fetching data: " + ex.Message);
            }
        }

        public UsersDTO getUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public VerificatorDTO getVerificator(int verID)
        {
            VerificatorDTO VerificatorDTO = new VerificatorDTO();
            var applicants = _administratorDAL.getVerificator(verID);
            var verificator = new VerificatorDTO
            {
                VerificatorID = applicants.VerificatorID,
                User = new UsersDTO
                {
                    UserID = applicants.User.UserID,
                    FirstName = applicants.User.FirstName,
                    MiddleName = applicants.User.MiddleName,
                    LastName = applicants.User.LastName,
                    UserEmail = applicants.User.UserEmail,
                },
                SKNumber = applicants.SKNumber,
                Position = applicants.Position,

            };

            return verificator;
        }

        public UsersDTO login(string email, string password)
        {
         
            try
            {
                var user = _administratorDAL.login(email, password);
                if (user == null)
                {
                    throw new ArgumentException("Email atau password anda salah");
                }
                var userTake = new UsersDTO
                {
                    FirstName = user.FirstName,
                    Role = new RoleDTO
                    {
                        RoleId= user.Role.RoleId,
                        RoleName = user.Role.RoleName
                    }
                };

                 return userTake;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error logging in: " + ex.Message);
            }
        }

        public void register(UsersDTO entity)
        {
            throw new NotImplementedException();
        }

        public void updateVerificator(UpdateVerificatorDTO entity)
        {
            try
            {
                var verificator = new VerificatorBO
                {
                    User = new DAL.BOs.UserBO
                    {
                        FirstName = entity.Users.FirstName,
                        MiddleName = entity.Users.MiddleName,
                        LastName = entity.Users.LastName,
                        UserID = entity.Users.UserID,
                    }
                };

                _administratorDAL.updateVerificator(verificator);
            }catch (Exception ex)
            {
                throw new ApplicationException("Error updating verificator: " + ex.Message);
            }
        }

        public void verifyAcademicData(int verificatorID, int UGDataID)
        {
            try
            {
                _administratorDAL.verifyAcademicData(verificatorID, UGDataID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error verifying academic data: " + ex.Message);
            }
        }

        public void verifyAchievementRecord(int verificatorID, int UGDataID)
        {
            try
            {
                _administratorDAL.verifyAchievementRecord(verificatorID, UGDataID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error verifying achievement record: " + ex.Message);
            }
        }

        public void verifyPersonalData(int verificatorID, int UGDataID)
        {
            try
            {
                _administratorDAL.verifyPersonalData(verificatorID, UGDataID);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error verifying personal data: " + ex.Message);
            }
        }
    }
}
