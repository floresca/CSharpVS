using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow
{
    class Program
    {
        static void Main(string[] args)
        {
            var commenter = new Post();
            commenter.GetComment();
            commenter.Vote();
        }
    }

    class Post
    {
        public string _postComment;
        string voteType;
        int voteCount;

        public void GetComment()
        {
            Console.Write("Enter your comment: ");
            _postComment = Console.ReadLine();

        }

        public void Vote()
        {
            while(true)
            {
                Console.Write("Vote on this comment: ");
                voteType = Console.ReadLine();
                UpOrDown();
            }
        }

        public void UpOrDown()
        {
            if(voteType == "up-vote")
            {
                voteCount++;
            }
            else if(voteType == "down-vote")
            {
                voteCount--;
            }
            else if (String.IsNullOrWhiteSpace(voteType))
            {
                Environment.Exit(0);
            }

            Publish();
        }

        public void Publish()
        {
            Console.WriteLine(_postComment);
            Console.WriteLine("Likes = {0}", voteCount);
        }
    }
}