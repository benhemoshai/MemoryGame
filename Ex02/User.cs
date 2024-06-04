using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    class User
    {
        private string m_UserName;
        private bool m_IsAI;
        private int m_UserScore;

        public User(string i_UserName, bool i_IsAiUser = false)
        {
            this.m_UserName = i_UserName;
            this.m_IsAI = i_IsAiUser;
            this.m_UserScore = 0;
        }

        public string UserName
        {
            get { return m_UserName; }
        } 

        public int UserScore
        { get { return m_UserScore; }
          set { m_UserScore = value; }
        }
        
    }
}
