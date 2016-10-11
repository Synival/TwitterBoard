using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterBoard {
   public class TwitterBoardException : Exception {
      public TwitterBoardException () : base () {
      }

      public TwitterBoardException (string message) : base (message) {
      }
   }
}