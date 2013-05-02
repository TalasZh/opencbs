// LICENSE PLACEHOLDER

using System;
using System.ComponentModel;
using System.Data.SqlClient;
using OpenCBS.CoreDomain;
using OpenCBS.CoreDomain.CreditScoring;
using OpenCBS.Enums;

namespace OpenCBS.Manager
{
    public class CreditScoringManager : Manager
    {
        public CreditScoringManager(User user) : base(user)
        {
        }

        public CreditScoringManager(string pTestDb) : base(pTestDb)
        {
        }

        public Question AddQuestion(Question question, SqlTransaction t)
        {
            const string q =
                @"INSERT INTO dbo.CreditScoringQuestions (question_name, question_type ) 
                               VALUES (@question_name, @question_type) 
                               SELECT SCOPE_IDENTITY()";


            using (OctopusCommand c = new OctopusCommand(q, t.Connection, t))
            {
                c.AddParam("@question_name", question.Name);
                c.AddParam("@question_type", (int) question.Type);
                question.Id = Convert.ToInt32(c.ExecuteScalar());

                if (question.Type == OQuestionType.Collection)
                {
                    foreach (Answer answer in question.Answers)
                    {
                        AddAnswer(answer, question.Id, t);
                    }
               }

               return question;
            }
        }

        public Question SaveQuestionValues(Question question, int loanId, SqlTransaction t)
        {
            const string q =
                @"INSERT INTO dbo.CreditScoringValues
                        ( question_name,
                          question_type,
                          answer_value,
                          score,
                          loan_id,
                          question_id
                        )
                VALUES  ( @question_name,
                          @question_type,
                          @answer_value,
                          @score,
                          @loan_id,
                          @question_id
                        ) 
                SELECT SCOPE_IDENTITY()";


            using (OctopusCommand c = new OctopusCommand(q, t.Connection, t))
            {
                c.AddParam("@question_name", question.Name);
                c.AddParam("@question_type", (int) question.Type);
                c.AddParam("@answer_value", question.AnswerValue);
                c.AddParam("@score", question.AnswerScore);
                c.AddParam("@question_id", question.Id);
                c.AddParam("@loan_id", loanId);
                question.Id = Convert.ToInt32(c.ExecuteScalar());

                if (question.Type == OQuestionType.Collection)
                {
                    foreach (Answer answer in question.Answers)
                    {
                        AddAnswer(answer, question.Id, t);
                    }
                }

                return question;
            }
        }

        public Question UpdateQuestionValues(Question question, int loanId, SqlTransaction t)
        {
            const string q =
                @"UPDATE dbo.CreditScoringValues
                  SET question_name = @question_name,
                          question_type = @question_type,
                          answer_value = @answer_value,
                          score = @score
                 WHERE loan_id = @loan_id 
                   AND question_id = @id";

            using (OctopusCommand c = new OctopusCommand(q, t.Connection, t))
            {

                c.AddParam("@question_name", question.Name);
                c.AddParam("@question_type", (int) question.Type);
                c.AddParam("@answer_value", question.AnswerValue);
                c.AddParam("@score", question.AnswerScore);
                c.AddParam("@loan_id", loanId);
                c.AddParam("@id", question.Id);
                question.Id = Convert.ToInt32(c.ExecuteScalar());

                if (question.Type == OQuestionType.Collection)
                {
                    foreach (Answer answer in question.Answers)
                    {
                        AddAnswer(answer, question.Id, t);
                    }
                }

                return question;

            }
        }

        public BindingList<Answer> SelectAnswers(int questionId)
        {
            BindingList<Answer> answers = new BindingList<Answer>();
            const string q = @"SELECT 
                                  a.id,          
                                  name,
                                  score,      
                                  credit_scoring_question_id
                                FROM dbo.CreditScoringAnswers a
                                WHERE a.credit_scoring_question_id = @id";

            using (OctopusCommand c = new OctopusCommand(q, GetConnection()))
            {
                c.AddParam("@id", questionId);

                using (OctopusReader r = c.ExecuteReader())
                {
                    if (r.Empty) return answers;

                    while (r.Read())
                    {
                        Answer answer = new Answer
                                            {
                                                Id = r.GetInt("id"),
                                                Name = r.GetString("name"),
                                                Score = r.GetInt("score")
                                            };
                        answers.Add(answer);
                    }
                }
                return answers;
            }
        }

        public QuestionList SelectQuestions()
        {
            QuestionList questionList = new QuestionList();
            const string q = @"SELECT 
                                  q.id,          
                                  question_name,
                                  question_type
                                FROM dbo.CreditScoringQuestions q";

            using(OctopusCommand c = new OctopusCommand(q, GetConnection()))
            using (OctopusReader r = c.ExecuteReader())
            {
                if (r.Empty) return questionList;

                while (r.Read())
                {
                    Question question = new Question()
                    {
                        Id = r.GetInt("id"),
                        Name = r.GetString("question_name"),
                        Type = (OQuestionType)r.GetInt("question_type")
                    };

                    questionList.Questions.Add(question);
                }
            }

            foreach (Question question in questionList.Questions)
            {
                if (question.Type == OQuestionType.Collection)
                {
                    question.Answers = SelectAnswers(question.Id);
                }
            }
            
            return questionList;
        }

        public QuestionList SelectValues(int loanId)
        {
            QuestionList questionList = new QuestionList();
            const string q = @"SELECT id,          
                                       question_name, 
                                       question_type,
                                       answer_value,
                                       score,
                                       loan_id,
                                       question_id
                                FROM dbo.CreditScoringValues
                                WHERE loan_id = @loan_id";

            using (OctopusCommand c = new OctopusCommand(q, GetConnection()))
            {
                c.AddParam("@loan_id", loanId);
                using (OctopusReader r = c.ExecuteReader())
                {
                    if (r.Empty) return questionList;

                    while (r.Read())
                    {
                        Question question = new Question()
                                                {
                                                    Id = r.GetInt("question_id"),
                                                    Name = r.GetString("question_name"),
                                                    Type = (OQuestionType) r.GetInt("question_type"),
                                                    AnswerValue = r.GetString("answer_value"),
                                                    AnswerScore = r.GetInt("score"),
                                                };

                        questionList.Questions.Add(question);
                    }
                }

                foreach (Question question in questionList.Questions)
                {
                    if (question.Type == OQuestionType.Collection)
                    {
                        question.Answers = SelectAnswers(question.Id);
                    }
                }

                return questionList;
            }
        }

        private void AddAnswer(Answer answer, int id, SqlTransaction t)
        {
            const string q = @"INSERT INTO dbo.CreditScoringAnswers (name, score, credit_scoring_question_id)
                               VALUES (@name, @score, @credit_scoring_question_id)";

            using (OctopusCommand c = new OctopusCommand(q, t.Connection, t))
            {
                c.AddParam("@name", answer.Name);
                c.AddParam("@score", answer.Score);
                c.AddParam("@credit_scoring_question_id", id);
                answer.Id = Convert.ToInt32(c.ExecuteScalar());
            }
        }

        private void DeleteAnswer(Question question, SqlTransaction t)
        {
            const string q = @"DELETE FROM dbo.CreditScoringAnswers 
                                WHERE credit_scoring_question_id = @credit_scoring_question_id";

            using (OctopusCommand c = new OctopusCommand(q, t.Connection, t))
            {
                c.AddParam("@credit_scoring_question_id", question.Id);
                c.ExecuteNonQuery();
            }
        }

        public void DeleteQuestion(Question question, SqlTransaction t)
        {
            DeleteAnswer(question, t);

            const string q = @"DELETE FROM dbo.CreditScoringQuestions WHERE id = @id";
            using (OctopusCommand c = new OctopusCommand(q, t.Connection, t))
            {
                c.AddParam("@id", question.Id);
                c.ExecuteNonQuery();
            }
        }

        public void UpdateQuestion(Question question, SqlTransaction t)
        {
            const string q = @"UPDATE dbo.CreditScoringQuestions
                               SET question_name = @question_name,
                                   question_type = @question_type
                               WHERE id = @id";

            using (OctopusCommand c = new OctopusCommand(q, t.Connection, t))
            {
                c.AddParam("@question_name", question.Name);
                c.AddParam("@question_type", (int) question.Type);
                c.AddParam("@id", question.Id);
                c.ExecuteNonQuery();

                DeleteAnswer(question, t);

                foreach (Answer answer in question.Answers)
                {
                    AddAnswer(answer, question.Id, t);
                }
            }
        }
    }
}
