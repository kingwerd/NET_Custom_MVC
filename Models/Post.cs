using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomMVC.Models {

    public class Post {
        
        public int PostId {get;set;}
        public string Title {get;set;}
        public string Content {get;set;}
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}
    }
}