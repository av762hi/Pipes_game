namespace Pipe_game.Core
{
    public class Tile
    {
        private PipeFlow flow;

        public void setFlow(PipeFlow flow){
             this.flow = flow;
         }
         public PipeFlow getFlow(){
            return flow;
        } 
    }
}