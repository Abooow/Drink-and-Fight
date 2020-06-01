var ExplodeVideoParticles : ParticleSystem;
var SparkTrailsParticles : ParticleSystem;
var SparkParticles : ParticleSystem;
var ExplodeAudio : AudioSource;

function Update ()
{
   
   if (Input.GetButtonDown("Fire1")) //check to see if the left mouse was pushed.
   { 
   		// Stop any previous explosions
 		ExplodeVideoParticles.Clear();
    	SparkParticles.Clear();
    	SparkTrailsParticles.Clear();
    	Explosion();      
    }
       
}



function Explosion()
{
    
	ExplodeVideoParticles.Play();
    SparkParticles.Play();
    SparkTrailsParticles.Play();
    ExplodeAudio.Play();

    
}

