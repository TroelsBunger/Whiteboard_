import React from 'react';
import './PostCard.css';

export default function PostCard(post){

  //console.log(cardInput);

  const postContent = post.post;

  return(
    <div className="notepaper">
      <figure className="quote">
        <blockquote className="curly-quotes" cite="https://www.youtube.com/watch?v=qYLrc9hy0t0">
          {postContent.content}
        </blockquote>
        <figcaption className="quote-by">— {postContent.user.userName}</figcaption>
      </figure>
    </div>

  );
}