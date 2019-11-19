node('dotnet-22'){

   try { 

      def gitUser = 'dotnet-dotnettest-github'
      def gitBranch = 'master'
      def gitRepo = 'https://github.com/gawdamear/openshift-siebel-notes-sln.git'

      def workingFolder = "/tmp/workspace/${env.JOB_NAME}"
      def solutionName = "siebelnotes.sln"
      
      stage('Checkout') {
        git credentialsId: "${gitUser}", branch: "${gitBranch}", url: "${gitRepo}"
      }   

      stage('Restore') {
        restore(workingFolder)
      } 

      stage('Clean') {
        clean(workingFolder)
      }       

      stage('Build') {
        build(workingFolder)
      }    

      stage('Testing') {
        parallel (
            "Unit tests" : {
                unitTest(workingFolder)
            },
            "Integration tests" : {
                integrationTest(workingFolder) 
            }
        )
      }
      
      stage('Publish') {
        publish(workingFolder)
      }  

      /*
      stage('Start Build') {
        dir(workingFolder) {
          sh "oc new-build --name=notesapi dotnet:2.2 --binary=true"
          sh "oc start-build notesapi --from-dir=api/bin/Release/netcoreapp2.2/publish"
        }
      }   

      stage('Deploy Application') {
        dir(workingFolder) {
          input 'Deploy to test?'
          sh "oc new-app notesapi"
          sh "oc expose svc/notesapi"
        }
      } */             
    }
    finally {
      echo 'cleanup'
    }     
}

def restore(def workingFolder) {
    dir (workingFolder) {
      sh "dotnet restore"      
    }
}

def clean(def workingFolder) {
    dir (workingFolder) {
      sh "dotnet clean"      
    }
}

def build(def workingFolder) {
    dir (workingFolder) {
      sh "dotnet build --configuration Release"      
    }
}

def unitTest(def workingFolder) {
    dir(workingFolder) {
      "dotnet test --logger:xunit"
      step([$class: 'XUnitBuilder', testTimeMargin: '3000', thresholdMode: 1, thresholds: [], 
      tools: [xUnitDotNet(deleteOutputFiles: true, failIfNotNew: false, 
      pattern: '**/TestResults/*.xml', skipNoTestFiles: false, stopProcessingIfError: true)]])   
    }
 }

def integrationTest(def workingFolder) {
    dir(workingFolder) {
      echo "integration testing..."
    }
}

def publish(def workingFolder) {
    dir(workingFolder) {
      sh "dotnet publish api/api.csproj -c Release /p:MicrosoftNETPlatformLibrary=Microsoft.NETCore.App"
    }
}
