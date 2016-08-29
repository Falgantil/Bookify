import bookifyapi from '../../util/bookifyapi';
import {observable, computed} from "mobx";

class FeedbackFormViewModel {
@observable bookId = 0;
@observable text = '';
@observable rating = '';

  constructor(bookId) {
    this.bookId = bookId;
  }

  async submit() {
    var result = await bookifyapi.postBookFeedback(this.bookId, this.text, this.rating);
    return result;
  }
}

export default FeedbackFormViewModel;