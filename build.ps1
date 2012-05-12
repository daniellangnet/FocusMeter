properties {
    $base_dir = resolve-path .\
    $build_artifacts_dir = "$base_dir\build\"
    $tools_dir = "$base_dir\tools\"
    $deploy_dir = "$base_dir\deploy\"
    $global:config = "Debug"
}

task default -depends clean, compile, zip

task clean {
    rd $build_artifacts_dir -recurse -force  -ErrorAction SilentlyContinue | out-null
    mkdir $build_artifacts_dir  -ErrorAction SilentlyContinue  | out-null
}

task compile {
    exec { msbuild  $base_dir\FocusMeter.sln /t:Clean /t:rebuild /p:Configuration=$config /p:DeployOnBuild=true /v:q /nologo /p:OutDir=$build_artifacts_dir}
}

task zip {

    $deploy_archive = "$deploy_dir\focusmeter.zip"

    $old_directory = pwd
    cd $build_artifacts_dir
    
    if (!(Test-Path $deploy_dir)) {
        mkdir $deploy_dir
    }

    exec {
    & $tools_dir\zip.exe -9 -A -r `
      $deploy_archive `
      *.*
    }
    
    cd $old_directory
}